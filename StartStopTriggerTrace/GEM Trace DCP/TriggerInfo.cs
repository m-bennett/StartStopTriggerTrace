using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartStopTriggerTrace.Models;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    public class TriggerInfo
    {
        public string AssociatedTraceGuid { get; set; }
        public Event CollectionEvent { get; set; }

        public string Subscriber { get; set; }

        public string KafkaTopic { get; set; }

        public Equipment Equipment { get; set; }

        public bool IsStartTrigger { get; set; }

    }
}
