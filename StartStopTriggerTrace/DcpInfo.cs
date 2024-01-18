using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StartStopTriggerTrace.Models;

namespace StartStopTriggerTrace
{
	public class DcpInfo
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string EventId { get; set; }
		public string AlarmId { get; set; }
		public string DcpName { get; set; }
		public string Subscriber { get; set; }
		public string Interval { get; set; }
		public int CollectionCount { get; set; }
		public int GroupSize { get; set; }
		public bool IsCyclical { get; set; }
		public string Severity { get; set; }
		public string KafkaTopic { get; set; }
		public Equipment Equipment { get; set; }
		public RequestType RequestType { get; set; }
		public string AppKey { get; set; }
		public List<Parameter> Parameters { get; set; }
		public List<Event> Events { get; set; }
		public List<TriggerEventRequest> StartTriggers { get; set; }
		public List<TriggerEventRequest> StopTriggers { get; set; }

		public DcpInfo()
		{
			AppKey = ConfigurationManager.AppSettings.Get("AppKey");
		}
	}

}
