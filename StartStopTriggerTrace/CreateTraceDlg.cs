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
    public partial class CreateTraceDlg : Form
    {
        public CreateTraceDlg()
        {
            InitializeComponent();
        }

        private CreateTriggerDlg _createTriggerDlg;

        private void btnCreateStartTrigger_Click(object sender, EventArgs e)
        {
            _createTriggerDlg = new CreateTriggerDlg();
            _createTriggerDlg.ShowDialog();

            _createTriggerDlg.Dispose();
            _createTriggerDlg=null;
        }

        private void btnCreateStopTrigger_Click(object sender, EventArgs e)
        {
            _createTriggerDlg = new CreateTriggerDlg();
            _createTriggerDlg.ShowDialog();

            _createTriggerDlg.Dispose();
            _createTriggerDlg=null;
        }
    }
}
