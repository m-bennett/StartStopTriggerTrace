using SapienceDcpManager.Models;
using StartStopTriggerTrace.Models;
using System;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    public enum TriggerTypes
    {
        Start,
        Stop
    }


    public class GemTraceDcpTrigger
    {
        public GemTraceDcpTrigger()
        {
            DcpListener.Instance.DcpReceived += Listener_DcpReceived;
        }

        private void Listener_DcpReceived(string dataCollectionReport)
        {
            // ???

            var eventReport = (EventReport)J;
        }

        public TriggerTypes TriggerType { get; set; }

        public TriggerEventRequest EventRequest { get; }

        public event EventHandler TriggerReceived;
    }
}
