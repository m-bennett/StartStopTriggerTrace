using System;
using System.Net.Http;

namespace StartStopTriggerTrace
{
	public class LogMessageEventArgs : EventArgs
	{
		public HttpResponseMessage Resposne { get; set; }
		public string Operation { get; set; }

	}
}
