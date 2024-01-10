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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private CreateTraceDlg _createTraceDlg;

        private void btnCreateDcp_Click(object sender, EventArgs e)
        {
            _createTraceDlg = new CreateTraceDlg();
            _createTraceDlg.ShowDialog();
        }
    }
}
