using StartStopTriggerTrace.Models;
using System.Collections.Generic;

namespace StartStopTriggerTrace
{
	public class EquipmentConnectionListItem
	{
		public Equipment Equipment { get; set; }
		public ConnectionTypeEnum ConnectionType { get; set; }
		public override string ToString()
		{
			return $"{Equipment.Name} : {ConnectionType}";
		}

	}
}
