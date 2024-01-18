using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace
{
	public class Event
	{
		public Event(string name, string id)
		{
			Name = name;
			Id = id;
		}

		public string Name { get; set; }
		public string Id { get; set; }
		public string DisplayName
		{
			get
			{
				return $"{Name} : Id = {Id}";
			}
		}
	}
}
