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

namespace StartStopTriggerTrace
{
    public partial class CreateTraceDcpDlg : Form, ILogForm
    {
        private string traceId;

        public Equipment Equipment { get; set; }

        public List<Parameter> ParameterList { get; set; }

        public List<Event> EventList { get; set; }


        public CreateTraceDcpDlg()
        {
            InitializeComponent();
        }

        private void CreateTraceDlg_Load(object sender, EventArgs e)
        {
            lbParameters.DataSource = ParameterList;
            lbParameters.DisplayMember = "DisplayName";
            traceId = new Random().Next(0, 10000).ToString();
            txtTraceDescription.Text = Equipment.Name + " Trace " + traceId;
        }

        public event EventHandler<LogMessageEventArgs> CreatedLogMessage;

        private void btnCreateStartTrigger_Click(object sender, EventArgs e)
        {
            var createTriggerDlg = new CreateTriggerDlg()
            {
                CollectionEvents = EventList
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
                CollectionEvents = EventList,
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

        private async void btnCreateDcp_Click(object sender, EventArgs e)
        {
            var parameters = new List<Parameter>();
            foreach (Parameter item in lbParameters.SelectedItems)
            {
                parameters.Add(item);
            }
            var startTriggers = new List<TriggerEventRequest>();
            foreach (TriggerEventRequest item in lbStartTriggers.Items)
            {
                startTriggers.Add(item);
            }
            var stopTriggers = new List<TriggerEventRequest>();
            foreach (TriggerEventRequest item in lbStopTriggers.Items)
            {
                stopTriggers.Add(item);
            }
            var dcpInfo = new DcpInfo()
            {
                DcpName = $"Trace DCP {traceId}",
                Description = txtTraceDescription.Text,
                Id = traceId,
                RequestType = RequestType.Trace,
                Interval = tbPeriod.Text,
                CollectionCount = Convert.ToInt32(tbSamples.Text),
                GroupSize = Convert.ToInt32(tbGroupSize.Text),
                Equipment = Equipment,
                Parameters = parameters,
                StartTriggers = startTriggers.Count != 0 ? startTriggers : null,
                StopTriggers = stopTriggers.Count != 0 ? stopTriggers : null
            };
            var response = await SapienceApiHandler.Instance.CreateDcpFromManager(dcpInfo);
            RaiseCreatedLogMessage(response, "Create Trace DCP");
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
    }
}
