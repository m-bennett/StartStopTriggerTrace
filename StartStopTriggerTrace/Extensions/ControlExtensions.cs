using System;
using System.Windows.Forms;

namespace StartStopTriggerTrace.Extensions
{
    public static class ControlExtensions
    {
        public static void PerformSafeOperation(this Control control, Action action)
        {
            if( control == null )
            {
                throw new ArgumentNullException("control");
            }

            if( action == null )
            {
                throw new ArgumentNullException("action");
            }

            if( control.InvokeRequired )
            {
                control.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        public static void BeginSafeOperation(this Control control, Action action)
        {
            if( control == null )
            {
                throw new ArgumentNullException("control");
            }

            if( action == null )
            {
                throw new ArgumentNullException("action");
            }

            if( control.InvokeRequired )
            {
                control.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}
