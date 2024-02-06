using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SapienceDcpManager.Models;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using StartStopTriggerTrace.Extensions;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GemTraceDcpTrigger
    {
        private DcpInfo _dcpInfo;

        public GemTraceDcpTrigger()
        {
            DcpListener.Instance.DcpReceived += Listener_DcpReceived;
        }
        public GemTraceDcpTrigger(TriggerInfo ti)
        {
            var parameters = new List<Parameter>();
            IsStartTrigger = ti.IsStartTrigger;
            CollectionEvent = ti.CollectionEvent;
            Subscriber = ti.Subscriber;
            KafkaTopic = ti.KafkaTopic;
            Equipment = ti.Equipment;

            DcpListener.Instance.DcpReceived += Listener_DcpReceived;
            _dcpInfo = new DcpInfo();

        }

        public async Task<HttpResponseMessage> CreateDcpFromManager()
        {
            var parameters = new List<Parameter>();
            _dcpInfo = new DcpInfo();

            _dcpInfo.DcpName = $"{(IsStartTrigger ? "Start" : "Stop")}TriggerEvent_EventId_{CollectionEvent.Id}_{Guid.NewGuid()}";
            _dcpInfo.EventId = CollectionEvent.Id;
            _dcpInfo.Description = Description;
            _dcpInfo.Subscriber = Subscriber;
            _dcpInfo.Parameters = parameters;
            _dcpInfo.KafkaTopic = KafkaTopic;
            _dcpInfo.RequestType = RequestType.Event;
            _dcpInfo.Equipment = Equipment;

            var response = await SapienceApiHandler.Instance.CreateDcpFromManager(_dcpInfo);

            return response;
        }

        private string _associatedTraceGuid;
        [JsonProperty]
        public string AssociatedTraceGuid 
        {
            get { return _associatedTraceGuid; }
            set
            {
                _associatedTraceGuid = value;
                //_dcpInfo.Description = Description;
            }
        }

        private string _description;
        [JsonProperty]
        public string Description 
        { 
            get
            {
                _description = $"{(IsStartTrigger ? "Start" : "Stop")} trigger for GEM trace DCP. EventId {CollectionEvent?.Id}. " +
                $"Equipment {Equipment?.Name.Truncate(20)}. Trace {_associatedTraceGuid}";
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        internal void Delete()
        {
            DcpListener.Instance.DcpReceived -= Listener_DcpReceived;
        }

        [JsonProperty]
        public Event CollectionEvent { get; set; }

        [JsonProperty]
        public string Subscriber { get; set; }

        [JsonProperty]
        public string KafkaTopic { get; set; }

        [JsonProperty]
        public Equipment Equipment { get; set; }

        private void Listener_DcpReceived(object sender, DcpListener.DcpReceivedEventArgs e)
        {
            if (e.Report.drsreport.report.eventId == CollectionEvent.Id)
            {
                TriggerReceived?.Invoke(this, EventArgs.Empty);
            }
        }

        [JsonProperty]
        public bool IsStartTrigger { get; set; }

        public event EventHandler TriggerReceived;
    }
}
