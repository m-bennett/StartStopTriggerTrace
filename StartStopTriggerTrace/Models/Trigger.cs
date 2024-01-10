using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.Models
{
	public class Trigger
	{
		public string triggerRequestType { get; set; }
		public string eventId { get; set; }
		public List<Condition> conditions { get; set; }
	}

    public class Condition
    {
        public string parameterId { get; set; }
        public string parameterType { get; set; }
        public string parameterValue { get; set; }
        public string @operator { get; set; }
    }
}
