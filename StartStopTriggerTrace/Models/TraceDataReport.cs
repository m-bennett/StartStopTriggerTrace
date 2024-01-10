using System.Collections.Generic;

namespace StartStopTriggerTrace.Models
{

    public class TraceDataReport
    {
        public string equipmentId { get; set; }
        public string connectionId { get; set; }
        public Drsreport drsreport { get; set; }
    }

    public class ParameterValue
    {
        public int data { get; set; }
        public string dataType { get; set; }
    }

    public class ParameterValueList
    {
        public string parameterId { get; set; }
        public string parameterName { get; set; }
        public ParameterValue parameterValue { get; set; }
    }

    public class Report
    {
        public string reportType { get; set; }
        public string timestamp { get; set; }
        public string traceId { get; set; }
        public List<ParameterValueList> parameterValueList { get; set; }
    }

    public class Drsreport
    {
        public string dataCollectionPlanId { get; set; }
        public Report report { get; set; }
    }

}
