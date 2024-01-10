using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.Models
{
	class prJobCreateEnhRequest
	{
        public int dataId { get; set; }
        public string prJobId { get; set; }
        public string mf { get; set; }
        public List<CarrierSlotList> carrierSlotList { get; set; }
        public ProcessJobRecipe processJobRecipe { get; set; }
        public bool prProcessStart { get; set; }
        public List<int> prPauseEvent { get; set; }
    }
    public class CarrierSlotList
    {
        public string carrierId { get; set; }
        public List<int> slotIds { get; set; }
    }

    public class ProcessJobRecipe
    {
        public string recipe { get; set; }
        public List<RecipeParameter> recipeParameters { get; set; }
        public int prRecipeMethod { get; set; }
    }

    public class Rcpparval
    {
        public int data { get; set; }
        public string dataType { get; set; }
        public object listData { get; set; }
    }

    public class RecipeParameter
    {
        public string rcpparnm { get; set; }
        public Rcpparval rcpparval { get; set; }
    }
}
