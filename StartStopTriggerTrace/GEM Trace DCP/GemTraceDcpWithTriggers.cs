using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    public class GemTraceDcpWithTriggers
    {
        public enum DcpStatus
        {
            Stopped,
            Started
        }
        
        public event EventHandler TraceCreated;
        public event EventHandler TraceDeleted;

        public GemTraceDcpWithTriggers(Guid traceId, Equipment equipment,
                                       IEnumerable<Parameter> parameters,
                                       IEnumerable<GemTraceDcpTrigger> triggers,
                                       string subscriber)
        {
            TraceId = traceId;
            Equipment = equipment;
            Parameters = parameters.ToList();
            Triggers = triggers.ToList();
            Subscriber = subscriber;

            // TODO - Create start/stop trigger DCP

            var trigger = new GemTraceDcpTrigger();
            trigger.TriggerReceived += Trigger_TriggerReceived;
        }

        private void Trigger_TriggerReceived(object sender, EventArgs e)
        {
            var trigger = (GemTraceDcpTrigger)sender;

            if (trigger.TriggerType == TriggerTypes.Start &&
                Status == DcpStatus.Stopped)
            {

                // TODO - Start trace

                Status = DcpStatus.Started;
            }
            else if (trigger.TriggerType == TriggerTypes.Stop &&
                Status == DcpStatus.Started)
            {
                // TODO - Stop trace
            }
        }

        public Guid TraceId { get; }

        public string Description { get; }

        public Equipment Equipment { get; }

        public List<Parameter> Parameters { get; }

        public string Subscriber { get; }

        public DcpStatus Status { get; private set; }

        public List<GemTraceDcpTrigger> Triggers { get; }
    }
}
