using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartStopTriggerTrace
{
    public partial class CreateTriggerDlg : Form
    {
        public List<Event> CollectionEvents { get; set; }

        public TriggerEventRequest TriggerResult { get; set; }


        public CreateTriggerDlg()
        {
            InitializeComponent();
        }

        private void CreateTriggerDlg_Load(object sender, EventArgs e)
        {
            lbEvents.DataSource = CollectionEvents;
            lbEvents.DisplayMember = "DisplayName";
        }

        private void btnAddTrigger_Click(object sender, EventArgs e)
        {
            TriggerResult = new TriggerEventRequest()
            {
                EventId = ((Event)lbEvents.SelectedItem).Id,
                TriggerRequestType = "TriggerEventRequest",
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
