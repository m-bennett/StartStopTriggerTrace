using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StartStopTriggerTrace.Models
{
	public class Request
	{
        public Request(string traceId = default(string), string alarmId = default(string), string eventId = default(string), RequestType requestType = default(RequestType), int? collectionIntervalMs = default(int?), List<ParameterRequest> parameterRequestsList = default(List<ParameterRequest>), int collectionCount = 0, int groupSize = 0, bool isCyclical = false)
        {
            TraceId = traceId;
            AlarmId = alarmId;
            EventId = eventId;
            RequestType = requestType;
            CollectionIntervalMs = collectionIntervalMs;
            ParameterRequestsList = parameterRequestsList ?? new List<ParameterRequest>();
            CollectionCount = collectionCount;
            GroupSize = groupSize;
            IsCyclical = isCyclical;
        }
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; private set; }

        /// <summary>
        /// Gets or Sets RequestType
        /// </summary>
        [DataMember(Name = "requestType", EmitDefaultValue = false)]
		[JsonProperty("requestType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public RequestType RequestType { get; set; }

        /// <summary>
        /// Gets or Sets Trace Id
        /// </summary>
        [DataMember(Name = "traceId", EmitDefaultValue = false)]
        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// Gets or Sets Event Id
        /// </summary>
        [DataMember(Name = "eventId", EmitDefaultValue = false)]
        [JsonProperty("eventId")]
        public string EventId { get; set; }

        /// <summary>
        /// Gets or Sets Alarm Id
        /// </summary>
        [DataMember(Name = "alarmId", EmitDefaultValue = false)]
        [JsonProperty("alarmId")]
        public string AlarmId { get; set; }

        /// <summary>
        /// Gets or Sets Severity 
        /// </summary>
        [DataMember(Name = "severity", EmitDefaultValue = false)]
        [JsonProperty("severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Gets or Sets CollectionIntervalMs
        /// </summary>
        [DataMember(Name = "collectionIntervalMs", EmitDefaultValue = false)]
        [JsonProperty("collectionIntervalMs")]
        public int? CollectionIntervalMs { get; set; }

        /// <summary>
        /// Gets or Sets the number of data collections reports for this request
        /// </summary>
        [DataMember(Name = "traceCollectionCount", EmitDefaultValue = true)]
        [JsonProperty("traceCollectionCount")]
        public int? CollectionCount { get; set; }

        /// <summary>
        /// Gets or Sets the number of data collection reports to be buffered in each DRS report
        /// </summary>
        [DataMember(Name = "traceGroupSize", EmitDefaultValue = true)]
        [JsonProperty("traceGroupSize")]
        public int? GroupSize { get; set; }

        /// <summary>
        /// Gets or Sets whether the trace will restart on its own once it has completed sending all reports
        /// </summary>
        [DataMember(Name = "traceCyclical", EmitDefaultValue = true)]
        [JsonProperty("traceCyclical")]
        public bool? IsCyclical { get; set; }

        /// <summary>
        /// Gets or Sets ParameterRequestsList
        /// </summary>
        [DataMember(Name = "parameterRequestsList", EmitDefaultValue = false)]
        [JsonProperty("parameterRequestsList")]
        public List<ParameterRequest> ParameterRequestsList { get; set; }

        /// <summary>
        /// Gets or Sets ParameterRequestsList
        /// </summary>
        [DataMember(Name = "startTriggers", EmitDefaultValue = false)]
        [JsonProperty("startTriggers")]
        public List<TriggerEventRequest> StartTriggers { get; set; }

        /// <summary>
        /// Gets or Sets ParameterRequestsList
        /// </summary>
        [DataMember(Name = "stopTriggers", EmitDefaultValue = false)]
        [JsonProperty("stopTriggers")]
        public List<TriggerEventRequest> StopTriggers { get; set; }
    }

    public enum RequestType
	{
        None,
        [EnumMember(Value = "AlarmRequest")]
        Alarm,
        [EnumMember(Value = "EventRequest")]
        Event,
        [EnumMember(Value = "TraceRequest")]
        Trace,
    }
}
