using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.Models
{
    public class Parameter
    {
        public string cpname { get; set; }
        public string cpval { get; set; }
        public string type { get; set; }
    }

    public class RemoteCommand
    {
        public string rcmd { get; set; }
        public List<Parameter> parameters { get; set; }
    }

}
