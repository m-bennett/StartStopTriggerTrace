using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StartStopTriggerTrace.Models;

namespace StartStopTriggerTrace
{
	public class Parameter
	{
		public Parameter(string name, string id)
		{
			Name = name;
			Id = id;
		}

		public string Name { get; set; }
		public string Id { get; set; }

		[JsonIgnore]
		public TraceConditions.ParameterTypeEnum ParameterType { get; set; }
		public string DisplayName
		{
			get
			{
				return $"{Name} : Id = {Id}";
			}
		}
	}
}
