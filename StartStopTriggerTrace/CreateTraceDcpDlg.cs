using Newtonsoft.Json;
using StartStopTriggerTrace.GEM_Trace_DCP;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StartStopTriggerTrace
{
    public partial class CreateTraceDcpDlg : Form, ILogForm
    {
        private GemHelper.DataLists _data;

        public Equipment Equipment { get; set; }

        private List<Parameter> _selectedParameters = new List<Parameter>();
        private bool _filteringParameters = false;

        public string Subscriber { get; set; }

        public List<Event> EventList { get; set; }

        public GemTraceDcpWithTriggers CreatedTrace { get; private set; } = null;

        public CreateTraceDcpDlg()
        {
            InitializeComponent();
        }

        private async void CreateTraceDlg_Load(object sender, EventArgs e)
        {
            cbEquipment.DisplayMember = "Name";
            await GetEquipment();
        }

        public event EventHandler<LogMessageEventArgs> CreatedLogMessage;

        private void btnCreateStartTrigger_Click(object sender, EventArgs e)
        {
            var createTriggerDlg = new CreateTriggerDlg()
            {
                CollectionEvents = _data.EventList
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
                CollectionEvents = _data.EventList,
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

            var tid = Guid.NewGuid().ToString();
            CreatedTrace = new GemTraceDcpWithTriggers(tid, Equipment, 
                                                        _selectedParameters, txtKafkaTopic.Text,
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
            if (response.IsSuccessStatusCode)
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
            }
            else
            {
                Log.Instance.WriteLog($"Sapience GET Equipment request failed. {response.Content.ReadAsStringAsync()}");
            }
        }

        private async void cbEquipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = (ComboBox)sender;
            if (cb.SelectedItem == null)
                return;

            btnCreateDcp.Enabled = false;

            try
            {
                Log.Instance.WriteLog("Retrieving equipment data...please wait...");
                lbStartTriggers.Items.Clear();
                lbStopTriggers.Items.Clear();
                Equipment = (Equipment)cb.SelectedItem;

                var equipmentConnection = Equipment.Connections
                    .Find(x => (ConnectionTypeEnum)Enum.Parse(typeof(ConnectionTypeEnum),
                               x.ConnectorDetail.CommunicationProtocol.Name) == ConnectionTypeEnum.GEM);

                var configFileId = equipmentConnection.EquipmentConnectionTemplate.ConfigurationFile.Id;

                _data = await GemHelper.GetEquipmentInfosAsync(configFileId);
                if (_data != null)
                {
                    lbParameters.BeginUpdate();

                    foreach (Parameter item in _data.ParameterList)
                        lbParameters.Items.Add(item);

                    lbParameters.DisplayMember = "DisplayName";

                    lbParameters.EndUpdate();

                    txtTraceDescription.Text = Equipment.Name + " Trace";


                    btnCreateDcp.Enabled = true;
                    Log.Instance.WriteLog("Equipment data retrieved successfully.");
                }
                else
                {
                    Log.Instance.WriteLog("Error retrieving equipment data.");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.WriteLog($"Error retrieving equipment data. {ex.Message}");
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            lbParameters.BeginUpdate();
            _filteringParameters = true;

            lbParameters.Items.Clear();

            if (string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                // Display all parameters.
                foreach (Parameter item in _data.ParameterList)
                    lbParameters.Items.Add(item);
            }
            else
            {
                // Display parameters that match the filter.
                var filter = txtFilter.Text.ToUpper();

                foreach (Parameter item in _data.ParameterList)
                {
                    if (item.DisplayName.ToUpper().Contains(filter))
                        lbParameters.Items.Add(item);
                }
            }

            for (int idx = 0; idx < lbParameters.Items.Count; ++idx)
            {
                if (_selectedParameters.Contains((Parameter)lbParameters.Items[idx]))
                    lbParameters.SetSelected(idx, true);
            }

            _filteringParameters = false;
            lbParameters.EndUpdate();
        }

        private void lbParameters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_filteringParameters)
                return;

            for (int idx = 0; idx < lbParameters.Items.Count; ++idx)
            {
                if (lbParameters.SelectedIndices.Contains(idx))
                {
                    // Parameter is selected. Make sure tht it is in _selectedParameters.

                    var item = (Parameter)lbParameters.Items[idx];

                    if (_selectedParameters.Contains(item) == false)
                        _selectedParameters.Add(item);
                }
                else
                {
                    // Parameter is not selected. Make sure that it is not in _selectedParameters.

                    var item = (Parameter)lbParameters.Items[idx];
                    _selectedParameters.Remove(item);
                }
            }
        }
    }
}
