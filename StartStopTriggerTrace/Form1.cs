using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SapienceDcpManager.Models;
using StartStopTriggerTrace.Extensions;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StartStopTriggerTrace
{
    public partial class Form1 : Form
    {
        private List<Parameter> parameterList;
        private List<Event> eventList;

        public Form1()
        {
            InitializeComponent();

            DcpReceived += Form_DcpReceived;

            Task.Run(StartHttpListener);

            parameterList = new List<Parameter>();
            eventList = new List<Event>();
        }

        private void Form_DcpReceived(object sender, string e)
        {
            
        }

        private HttpListener listener;

        private event EventHandler<string> DcpReceived;

        private bool running = true;

        private void StartHttpListener()
        {
            listener = new HttpListener();
            var prefix = string.Format(SapienceApiHandler.Instance.EndpointURL);
            listener.Prefixes.Add(prefix);
            try
            {
                listener.Start();

                while (running)
                {
                    var context = listener.GetContext();
                    Console.WriteLine("*********** Begin receive data ****************");

                    var request = context.Request;
                    var response = context.Response;
                    var requestData = GetRequestPostData(request);

                    var eventReport = JsonConvert.DeserializeObject<EventReport>(requestData);

                    Console.WriteLine("*********** End receive data ****************");
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();
                    DcpReceived?.Invoke(this, requestData);
                }

                listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //requestResultTb.PerformSafeOperation(() =>
                //{
                //    requestResultTb.AppendText("HTTP Listener Failed to Start:");
                //    requestResultTb.AppendText("\r\n");
                //    requestResultTb.AppendText(e.ToString());
                //});
            }
        }


        public string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void btnCreateTraceDcp_Click(object sender, EventArgs e)
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
            }
            else
            {
                MessageBox.Show($"Connection type {equipmentConnection.ConnectionType} does not support Trace.");
            }

            TraceDcpForm.CreatedLogMessage += OnMessageLog;
            TraceDcpForm.ShowDialog(this);
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
