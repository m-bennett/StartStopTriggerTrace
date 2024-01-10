using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.Models
{
	class CreateObjectRequest
    {
        public string objspec { get; set; }
        public string objtype { get; set; }
        public List<AttributeRequest> attributeRequests { get; set; }
    }
    public class Attrdata
    {
        public dynamic data { get; set; }
        public string dataType { get; set; }
        public List<Attrdata> listData { get; set; }
    }
    public class AttributeRequest
    {
        public string attrid { get; set; }
        public Attrdata attrdata { get; set; }
  	}
}
