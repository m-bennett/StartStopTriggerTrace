using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StartStopTriggerTrace
{
    public partial class CreateTriggerDlg : Form
    {
        private Event _selectedEvent = null;
        private bool _filteringEvents = false;

        public List<Event> CollectionEvents { get; set; }

        public Event TriggerResult { get; set; }


        public CreateTriggerDlg()
        {
            InitializeComponent();
        }

        private void CreateTriggerDlg_Load(object sender, EventArgs e)
        {
            lbEvents.BeginUpdate();

            foreach (Event item in CollectionEvents)
                lbEvents.Items.Add(item);

            lbEvents.DisplayMember = "DisplayName";

            lbEvents.EndUpdate();
        }

        private void btnAddTrigger_Click(object sender, EventArgs e)
        {
            TriggerResult = _selectedEvent;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            TriggerResult = null;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            lbEvents.BeginUpdate();
            _filteringEvents = true;

            lbEvents.Items.Clear();

            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                // Display all events.
                foreach (Event item in CollectionEvents)
                    lbEvents.Items.Add(item);
            }
            else
            {
                // Display events that match the filter.
                var filter = txtFilter.Text.ToUpper();

                foreach (Event item in CollectionEvents)
                {
                    if (item.DisplayName.ToUpper().Contains(filter))
                        lbEvents.Items.Add(item);
                }
            }

            if (_selectedEvent != null)
            {
                for (int idx = 0; idx < lbEvents.Items.Count; ++idx)
                {
                    if (_selectedEvent == ((Event)lbEvents.Items[idx]))
                    {
                        lbEvents.SetSelected(idx, true);
                        break;
                    }
                }
            }

            _filteringEvents = false;
            lbEvents.EndUpdate();
        }

        private void lbEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_filteringEvents)
                return;

            _selectedEvent = lbEvents.SelectedIndex == -1 ? null : (Event)lbEvents.SelectedItem;
        }
    }
}
