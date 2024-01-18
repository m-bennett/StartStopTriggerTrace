using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace
{
	interface ILogForm
	{
		event EventHandler<LogMessageEventArgs> CreatedLogMessage;
		DialogResult ShowDialog(IWin32Window owner);
	}
}
