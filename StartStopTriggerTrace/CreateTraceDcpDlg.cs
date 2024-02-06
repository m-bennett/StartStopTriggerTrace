﻿using Newtonsoft.Json;
using StartStopTriggerTrace.GEM_Trace_DCP;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StartStopTriggerTrace
{
    public partial class CreateTraceDcpDlg : Form, ILogForm
    {
        private string traceId;
        private List<Parameter> parameterList;
        private List<Event> eventList;


        public Equipment Equipment { get; set; }

        public List<Parameter> ParameterList { get; set; }

        public string Subscriber { get; set; }

        public List<Event> EventList { get; set; }

        public GemTraceDcpWithTriggers CreatedTrace { get; private set; } = null;

        public CreateTraceDcpDlg()
        {
            InitializeComponent();
        }

        private async void CreateTraceDlg_Load(object sender, EventArgs e)
        {
            parameterList = new List<Parameter>();
            eventList = new List<Event>();
            //traceId = new Random().Next(0, 10000).ToString();
            txtSubscriber.Text = Subscriber;
            cbEquipment.DisplayMember = "Name";
            await GetEquipment();
        }

        public event EventHandler<LogMessageEventArgs> CreatedLogMessage;

        private void btnCreateStartTrigger_Click(object sender, EventArgs e)
        {
            var createTriggerDlg = new CreateTriggerDlg()
            {
                CollectionEvents = eventList
            };

            if (createTriggerDlg.ShowDialog(this) == DialogResult.OK)
            {
                lbStartTriggers.DisplayMember = "DisplayName";
                lbStartTriggers.Items.Add(createTriggerDlg.TriggerResult);
            }
        }

        private void btnCreateStopTrigger_Click(object sender, EventArgs e)
        {
            var createTriggerDlg = new CreateTriggerDlg()
            {
                CollectionEvents = eventList,
            };

            if (createTriggerDlg.ShowDialog(this) == DialogResult.OK)
            {
                lbStopTriggers.DisplayMember = "DisplayName";
                lbStopTriggers.Items.Add(createTriggerDlg.TriggerResult);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreateDcp_Click(object sender, EventArgs e)
        {
            var triggers = new List<GemTraceDcpTrigger>();

            // Start triggers
            foreach (Event item in lbStartTriggers.Items)
            {
                var triggerinfo = new TriggerInfo
                {
                    IsStartTrigger = true,
                    CollectionEvent = item,
                    Subscriber = Subscriber,
                    KafkaTopic = txtKafkaTopic.Text,
                    Equipment = Equipment
                };
                triggers.Add(new GemTraceDcpTrigger(triggerinfo));
            }

            // Stop triggers
            foreach (Event item in lbStopTriggers.Items)
            {
                var triggerinfo = new TriggerInfo
                {
                    IsStartTrigger = false,
                    CollectionEvent = item,
                    Subscriber = Subscriber,
                    KafkaTopic = txtKafkaTopic.Text,
                    Equipment = Equipment
                };
                triggers.Add(new GemTraceDcpTrigger(triggerinfo));
            }

            var parameters = new List<Parameter>();
            foreach (Parameter item in lbParameters.SelectedItems)
            {
                parameters.Add(item);
            }

            var tid = Guid.NewGuid().ToString();
            CreatedTrace = new GemTraceDcpWithTriggers(tid, Equipment, 
                                                        parameters, txtKafkaTopic.Text,
                                                        $"{txtTraceDescription.Text} {tid}",
                                                        triggers, tbPeriod.Text);

            foreach(var t in CreatedTrace.Triggers)
            {
                t.AssociatedTraceGuid = CreatedTrace.Id;
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }

        protected void RaiseCreatedLogMessage(HttpResponseMessage response, string operation)
        {
            CreatedLogMessage?.Invoke(this, new LogMessageEventArgs
            {
                Resposne = response,
                Operation = operation
            });
        }

        private void btnDeleteStartTrigger_Click(object sender, EventArgs e)
        {
            if (lbStartTriggers.SelectedItem == null)
                return;

            lbStartTriggers.Items.Remove(lbStartTriggers.SelectedItem);
        }

        private void btnDeleteStopTrigger_Click(object sender, EventArgs e)
        {
            if (lbStopTriggers.SelectedItem == null)
                return;

            lbStopTriggers.Items.Remove(lbStopTriggers.SelectedItem);
        }

        private async Task GetEquipment()
        {
            var response = await SapienceApiHandler.Instance.GetEquipment();
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var equipment = JsonConvert.DeserializeObject<EquipmentResponse>(jsonString);

                cbEquipment.Items.Clear();

                foreach (Equipment eq in equipment.Content)
                {
                    foreach (EquipmentConnection connection in eq.Connections)
                    {
                        var connectionType = (ConnectionTypeEnum)Enum.Parse(typeof(ConnectionTypeEnum), ConnectionType.ConnectionTypeMappings[connection.ConnectorDetail.CommunicationProtocol.Name], true);

                        if(connectionType == ConnectionTypeEnum.GEM)

                        cbEquipment.Items.Add(eq);
                    }
                }
                cbEquipment.SelectedIndex = 0;
                //cbEquipment_SelectedIndexChanged(cbEquipment, new EventArgs());

            }
        }

        private async void cbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb.SelectedItem == null)
                return;

            //btnCreateTraceDcp.Enabled = false;

            try
            {
                lbStartTriggers.Items.Clear();
                lbStopTriggers.Items.Clear();
                Equipment = (Equipment)cb.SelectedItem;

                var equipmentConnection = Equipment.Connections
                    .Find(x => (ConnectionTypeEnum)Enum.Parse(typeof(ConnectionTypeEnum),
                               x.ConnectorDetail.CommunicationProtocol.Name) == ConnectionTypeEnum.GEM);

                var configFileId = equipmentConnection.EquipmentConnectionTemplate.ConfigurationFile.Id;

                await GetEquipmentInfosAsync(configFileId);
                txtTraceDescription.Text = Equipment.Name + " Trace";


                //btnCreateTraceDcp.Enabled = true;
            }
            catch (Exception ex)
            {
                //tbLogs.PerformSafeOperation(() =>
                //{
                //    tbLogs.AppendText("Failed to load Equipment Config");
                //    tbLogs.AppendText("\r\n");
                //    tbLogs.AppendText(ex.ToString());
                //});
            }
        }

        private async Task GetEquipmentInfosAsync(string configFileId)
        {
            var response = await SapienceApiHandler.Instance.GetConfigurationFile(configFileId);
            if (response != null)
            {
                var xml = response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.Result);

                    ParseGemData(doc);

                lbParameters.DataSource = parameterList;
                lbParameters.DisplayMember = "DisplayName";
            }
        }
        private string GetElementSourceId(XmlElement element)
        {
            var parentNode = (XmlElement)element.ParentNode;
            if (parentNode.HasAttribute("SourceID"))
            {
                return parentNode.GetAttribute("SourceID");
            }
            else
            {
                return GetElementSourceId(parentNode);
            }
        }

        private void ParseGemData(XmlDocument doc)
        {
            parameterList.Clear();
            eventList.Clear();

            var StatusVariableList = doc.SelectNodes("//StatusVariables");
            foreach (XmlElement list in StatusVariableList)
            {
                var variableLists = list.ChildNodes;
                foreach (XmlElement parameter in variableLists)
                {
                    var name = parameter.SelectSingleNode("Name")?.InnerText;
                    var sourceId = parameter.SelectSingleNode("Id").InnerText;
                    var param = new Parameter(name, $"{sourceId}");
                    parameterList.Add(param);
                }
            }

            var EcVariableLists = doc.SelectNodes("//EquipmentConstants");
            foreach (XmlElement list in EcVariableLists)
            {
                var variableDescriptions = list.SelectNodes("//EquipmentConstantDefinition");
                foreach (XmlElement parameter in variableDescriptions)
                {
                    var name = parameter.SelectSingleNode("Name")?.InnerText;
                    var sourceId = parameter.SelectSingleNode("Id").InnerText;
                    var param = new Parameter(name, $"{sourceId}");
                    parameterList.Add(param);
                }
            }
            parameterList = parameterList.OrderBy(x => x.Name).ToList();


            var eventLists = doc.SelectNodes("//CollectionEvents");
            foreach (XmlElement list in eventLists)
            {
                var eventDescription = list.SelectNodes("//CollectionEventDescription");
                foreach (XmlElement ev in eventDescription)
                {
                    var name = ev.SelectSingleNode("Name")?.InnerText;
                    var sourceId = ev.SelectSingleNode("Id").InnerText;
                    var collectionEvent = new Event(name, $"{sourceId}");
                    eventList.Add(collectionEvent);
                }
            }
            eventList = eventList.OrderBy(x => x.Name).ToList();
        }



    }
}
