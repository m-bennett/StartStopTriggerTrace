using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SapienceDcpManager.Models;
using StartStopTriggerTrace.Extensions;
using StartStopTriggerTrace.GEM_Trace_DCP;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace StartStopTriggerTrace
{
    public partial class Form1 : Form
    {
        private List<Parameter> parameterList;
        private List<Event> eventList;
        private readonly List<GemTraceDcpWithTriggers> traces = new List<GemTraceDcpWithTriggers>();

        public Form1()
        {
            InitializeComponent();

            var listener = DcpListener.Instance;
            listener.StartListening();

            if (File.Exists("StartStopTriggerTraceDcpList.json"))
            {
                traces = JsonConvert.DeserializeObject<List<GemTraceDcpWithTriggers>>(File.ReadAllText("StartStopTriggerTraceDcpList.json"));
            }

            lbDcps.DisplayMember = "Description";

            foreach (var trace in traces)
                lbDcps.Items.Add(trace);

            parameterList = new List<Parameter>();
            eventList = new List<Event>();
        }

        private async void btnCreateTraceDcp_Click(object sender, EventArgs e)
        {
            var equipmentConnection = (EquipmentConnectionListItem)cbEquipment.SelectedItem;

            ILogForm TraceDcpForm = null;
            if (equipmentConnection.ConnectionType == ConnectionTypeEnum.GEM)
            {
                TraceDcpForm = new CreateTraceDcpDlg()
                {
                    Equipment = equipmentConnection.Equipment,
                    Text = $"Create Trace for {equipmentConnection.Equipment.Name}",
                    ParameterList = parameterList,
                    Subscriber = SapienceApiHandler.Instance.EndpointURL,
                    EventList = eventList
                };

                TraceDcpForm.CreatedLogMessage += OnMessageLog;

                if (TraceDcpForm.ShowDialog(this) == DialogResult.OK)
                {
                    var trace = ((CreateTraceDcpDlg)TraceDcpForm).CreatedTrace;
                    lbDcps.Items.Add(trace);
                    traces.Add(trace);

                    var json = JsonConvert.SerializeObject(traces, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            NullValueHandling = NullValueHandling.Ignore
                        });
                    
                    File.WriteAllText($"StartStopTriggerTraceDcpList.json", json);

                    await trace.Start();
                }
            }
            else
            {
                MessageBox.Show($"Connection type {equipmentConnection.ConnectionType} does not support Trace.");
            }

            
        }

        private void OnMessageLog(object sender, LogMessageEventArgs e)
        {
            WriteToConsole(e.Resposne, e.Operation);
        }

        private void WriteToConsole(HttpResponseMessage response, string operation = "")
        {
            var result = response.Content.ReadAsStringAsync().Result;
            try
            {
                result = JValue.Parse(result).ToString(Newtonsoft.Json.Formatting.Indented);
            }
            catch (Newtonsoft.Json.JsonReaderException) { }

            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine($"=========={operation}==============");
            builder.AppendLine($"HTTP code: {(int)response.StatusCode} {response.StatusCode} ");
            builder.AppendLine(result);
            tbLogs.AppendText(builder.ToString());
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
                        var connectionListItem = new EquipmentConnectionListItem()
                        {
                            Equipment = eq,
                            ConnectionType = (ConnectionTypeEnum)Enum.Parse(typeof(ConnectionTypeEnum), ConnectionType.ConnectionTypeMappings[connection.ConnectorDetail.CommunicationProtocol.Name], true)
                        };

                        cbEquipment.Items.Add(connectionListItem);
                        cbEquipment.SelectedIndex = 0;
                    }
                }
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await GetEquipment();
        }

        private async void cbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb.SelectedItem == null)
                return;

            btnCreateTraceDcp.Enabled = false;

            try
            {
                var connection = (EquipmentConnectionListItem)cb.SelectedItem;

                var equipmentConnection = connection.Equipment.Connections
                    .Find(x => (ConnectionTypeEnum)Enum.Parse(typeof(ConnectionTypeEnum),
                               x.ConnectorDetail.CommunicationProtocol.Name) == connection.ConnectionType);
                
                var configFileId = equipmentConnection.EquipmentConnectionTemplate.ConfigurationFile.Id;
                
                await GetEquipmentInfosAsync(configFileId, connection.ConnectionType);

                eventList = eventList.OrderBy(x => x.Name).ToList();

                // GetDcps();

                btnCreateTraceDcp.Enabled = true;
            }
            catch (Exception ex)
            {
                tbLogs.PerformSafeOperation(() =>
                {
                    tbLogs.AppendText("Failed to load Equipment Config");
                    tbLogs.AppendText("\r\n");
                    tbLogs.AppendText(ex.ToString());
                });
            }
        }

        private async Task GetEquipmentInfosAsync(string configFileId, ConnectionTypeEnum connType)
        {
            var response = await SapienceApiHandler.Instance.GetConfigurationFile(configFileId);
            if (response != null)
            {
                var xml = response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.Result);

                if (connType == ConnectionTypeEnum.EDA)
                    ParseEdaData(doc);
                else
                    ParseGemData(doc);

                parameterList = parameterList.OrderBy(x => x.Name).ToList();
            }
        }

        private void ParseEdaData(XmlDocument doc)
        {
            var parameters = doc.SelectNodes("//*[@Type='Parameter']");
            parameterList.Clear();
            foreach (XmlElement parameter in parameters)
            {
                var name = parameter.GetAttribute("Name");
                var sourceId = GetElementSourceId(parameter);
                var param = new Parameter(name, $"{sourceId}:{name}");
                parameterList.Add(param);
            }

            var events = doc.SelectNodes("//*[@Type='Event']");
            eventList.Clear();
            foreach (XmlElement cevent in events)
            {
                XmlElement sourceNode = (XmlElement)cevent.ParentNode.ParentNode;
                var name = cevent.GetAttribute("Name");
                var sourceId = GetElementSourceId(cevent);
                sourceNode.GetAttribute("SourceID");
                var ev = new Event(name, $"{sourceId}:{name}");
                eventList.Add(ev);
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

            var variableLists = doc.SelectNodes("//VariableDescription");
            foreach (XmlElement parameter in variableLists)
            {
                var name = parameter.SelectSingleNode("Name")?.InnerText;
                var sourceId = parameter.SelectSingleNode("Id").InnerText;
                var param = new Parameter(name, $"{sourceId}");
                parameterList.Add(param);
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
        }

        private async void btnDeleteDcp_Click(object sender, EventArgs e)
        {
            if (lbDcps.SelectedItem == null)
                return;

            var dcpId = ((DataCollectionPlan)lbDcps.SelectedItem).Id;
            var response = await SapienceApiHandler.Instance.DeleteDcp(dcpId);
            WriteToConsole(response, $"Delete DCP {dcpId}");
            GetDcps();
        }

        private async void GetDcps()
        {
            lbDcps.Items.Clear();

            var response = await SapienceApiHandler.Instance.GetDcps(null);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var dcpList = JsonConvert.DeserializeObject<DataCollectionPlanResponse>(jsonString);

                foreach (DataCollectionPlan dcp in dcpList.Content)
                {
                    lbDcps.Items.Add(dcp);
                }

                lbDcps.DisplayMember = "Name";
            }
        }
    }
}
