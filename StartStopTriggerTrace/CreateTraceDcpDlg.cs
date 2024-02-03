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

namespace StartStopTriggerTrace
{
    public partial class CreateTraceDcpDlg : Form, ILogForm
    {
        private string traceId;

        public Equipment Equipment { get; set; }

        public List<Parameter> ParameterList { get; set; }

        public string Subscriber { get; set; }

        public List<Event> EventList { get; set; }

        public GemTraceDcpWithTriggers CreatedTrace { get; private set; } = null;

        public CreateTraceDcpDlg()
        {
            InitializeComponent();
        }

        private void CreateTraceDlg_Load(object sender, EventArgs e)
        {
            lbParameters.DataSource = ParameterList;
            lbParameters.DisplayMember = "DisplayName";
            traceId = new Random().Next(0, 10000).ToString();
            txtTraceDescription.Text = Equipment.Name + " Trace";
            txtSubscriber.Text = Subscriber;
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

        private void btnCreateDcp_Click(object sender, EventArgs e)
        {
            var triggers = new List<GemTraceDcpTrigger>();

            // Start triggers
            foreach (Event item in lbStartTriggers.Items)
            {
                triggers.Add(new GemTraceDcpTrigger(true,
                                    item, Subscriber, Equipment));
            }

            // Stop triggers
            foreach (Event item in lbStopTriggers.Items)
            {
                triggers.Add(new GemTraceDcpTrigger(false,
                                    item, Subscriber, Equipment));
            }

            var parameters = new List<Parameter>();
            foreach (Parameter item in lbParameters.SelectedItems)
            {
                parameters.Add(item);
            }

            CreatedTrace = new GemTraceDcpWithTriggers(Equipment, parameters, txtTraceDescription.Text,
                                                triggers, tbPeriod.Text);
            
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
    }
}
