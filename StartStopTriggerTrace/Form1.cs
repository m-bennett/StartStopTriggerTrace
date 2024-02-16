using Newtonsoft.Json;
using StartStopTriggerTrace.Extensions;
using StartStopTriggerTrace.GEM_Trace_DCP;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

using Formatting = Newtonsoft.Json.Formatting;

namespace StartStopTriggerTrace
{
    public partial class Form1 : Form
    {
        private List<Parameter> parameterList;
        private List<Event> eventList;
        private readonly List<GemTraceDcpWithTriggers> traces = new List<GemTraceDcpWithTriggers>();
        private string appkey = ConfigurationManager.AppSettings.Get("AppKey");
        private string TraceDcpSaveFileName = "StartStopTriggerTraceDcpList.json";



        public Form1()
        {
            InitializeComponent();

            Log.Instance.MessageLogged += MessageLogged;
            Log.Instance.WriteLog("StartStopTriggerTrace app started...");

            if (File.Exists(TraceDcpSaveFileName))
            {
                traces = JsonConvert.DeserializeObject<List<GemTraceDcpWithTriggers>>(File.ReadAllText(TraceDcpSaveFileName));
                Log.Instance.WriteLog($"{traces.Count} Traces deserialized successfully.");
            }

            lbDcps.DisplayMember = "Description";

            foreach (var trace in traces)
                lbDcps.Items.Add(trace);

            parameterList = new List<Parameter>();
            eventList = new List<Event>();

            SyncWithServer();

        }


        private void MessageLogged(string message)
        {
            tbLogs.PerformSafeOperation(() => tbLogs.AppendText(message));
        }

        private async void btnCreateTraceDcp_Click(object sender, EventArgs e)
        {
            ILogForm TraceDcpForm = null;
            TraceDcpForm = new CreateTraceDcpDlg()
            {
                Text = $"Create Trace",
                EventList = eventList
            };

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

                SaveTraces(json);

                await trace.Start();
            }
        }

        private async void SyncWithServer()
        {
            var response = await SapienceApiHandler.Instance.GetDcps(appkey);
            if (response.IsSuccessStatusCode)
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

            SaveTraces(json);
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
                Equipment = trace.Equipment,
                CreatedTrace = trace

            };

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

                SaveTraces(json);

                await newtrace.Start();
            }

        }

        private void SaveTraces(string json)
        {
            File.WriteAllText(TraceDcpSaveFileName, json);
            Log.Instance.WriteLog("Traces serialized successfully.");
        }

        private void btnClearLogs_Click(object sender, EventArgs e)
        {
            tbLogs.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Log.Instance.WriteLog("StartStopTriggerTrace app shutting down...");
        }
    }
}
