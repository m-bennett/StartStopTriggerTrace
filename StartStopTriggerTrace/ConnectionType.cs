using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace
{
	public class ConnectionType
	{
		public static Dictionary<string, string> ConnectionTypeMappings = new Dictionary<string, string>
		{
			{"GEM", "GEM" },
			{"EDA", "EDA" },
			{"OPCUA", "OPCUA" },
			{"OPC-UA", "OPCUA" },
			{"MQTT", "MQTT" }
		};
	}
	public enum ConnectionTypeEnum
	{
		GEM,
		EDA,
		OPCUA,
		MQTT
	}
}
