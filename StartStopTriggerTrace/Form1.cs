using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SapienceDcpManager.Models;
using StartStopTriggerTrace.Extensions;
using StartStopTriggerTrace.GEM_Trace_DCP;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private string appkey = ConfigurationManager.AppSettings.Get("AppKey");


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

            SyncWithServer();
        }

        private async void btnCreateTraceDcp_Click(object sender, EventArgs e)
        {
            ILogForm TraceDcpForm = null;
            TraceDcpForm = new CreateTraceDcpDlg()
            {
                Text = $"Create Trace",
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

        private async void SyncWithServer()
        {
            var response = await SapienceApiHandler.Instance.GetDcps(appkey);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var dcpList = JsonConvert.DeserializeObject<DataCollectionPlanResponse>(jsonString);

                foreach (var dcp in dcpList.Content)
                {
                    response = await SapienceApiHandler.Instance.DeleteDcp(dcp.Id);
                }

            }
            foreach (GemTraceDcpWithTriggers trace in lbDcps.Items)
            {
                await trace.Start();
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


        private async void btnDeleteDcp_Click(object sender, EventArgs e)
        {
            if (lbDcps.SelectedItem == null)
                return;

            var trace = (GemTraceDcpWithTriggers)lbDcps.SelectedItem;
            traces.Remove(trace);
            await trace.Delete();
            lbDcps.Items.Remove(trace);

            var json = JsonConvert.SerializeObject(traces, Formatting.Indented, 
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });

            File.WriteAllText($"StartStopTriggerTraceDcpList.json", json);
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

        private async void btnEditDcp_Click(object sender, EventArgs e)
        {
            if (lbDcps.SelectedItem == null)
                return;

            var trace = (GemTraceDcpWithTriggers)lbDcps.SelectedItem;
            ILogForm EditTraceDcpForm = null;
            EditTraceDcpForm = new EditTraceDcpDlg()
            {
                Text = $"Edit Trace",
                Subscriber = SapienceApiHandler.Instance.EndpointURL,
                Equipment = trace.Equipment,
                CreatedTrace = trace
                
            };

            EditTraceDcpForm.CreatedLogMessage += OnMessageLog;

            if (EditTraceDcpForm.ShowDialog(this) == DialogResult.OK)
            {
                //Delete old trace
                lbDcps.Items.Remove(trace);
                await trace.Delete();
                traces.Remove(trace);

                //Add new trace
                var newtrace = ((EditTraceDcpDlg)EditTraceDcpForm).CreatedTrace;
                lbDcps.Items.Add(newtrace);
                traces.Add(newtrace);

                var json = JsonConvert.SerializeObject(traces, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                File.WriteAllText($"StartStopTriggerTraceDcpList.json", json);

                await newtrace.Start();
            }

        }
    }
}
