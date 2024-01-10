using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.Models
{
    class CarrierActionRequest
    {
        public string carrierAction { get; set; }
        public string carrierId { get; set; }
        public int ptn { get; set; }
        public List<List<CarrierAttribute>> carrierAttributes { get; set; }
    }

    class CarrierAttribute
    {
        public string cattrid { get; set; }
        public Cattrdata cattrdata {get; set;}
    }
    class Cattrdata
	{
        public string data { get; set; }
        public string dataType { get; set; }
	}
}
