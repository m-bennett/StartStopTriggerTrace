using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SapienceDcpManager.Models;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GemTraceDcpTrigger
    {
        private DcpInfo _dcpInfo;

        public GemTraceDcpTrigger(bool isStartTrigger, Event collectionEvent,
                                  string subscriber, Equipment equipment)
        {
            var parameters = new List<Parameter>();
            IsStartTrigger = isStartTrigger;
            CollectionEvent = collectionEvent;
            Subscriber = subscriber;
            Equipment = equipment;

            var description = $"{(isStartTrigger ? "Start" : "Stop")} trigger for GEM trace DCP. Event {collectionEvent.Id} - {collectionEvent.Name}.";

            DcpListener.Instance.DcpReceived += Listener_DcpReceived;

            _dcpInfo = new DcpInfo()
            {
                DcpName = $"{Guid.NewGuid()}",
                EventId = collectionEvent.Id,
                Description = description,
                Subscriber = subscriber,
                Parameters = parameters,
                RequestType = RequestType.Event,
                Equipment = equipment
            };
        }

        public async Task<HttpResponseMessage> CreateDcpFromManager()
        {
            var response = await SapienceApiHandler.Instance.CreateDcpFromManager(_dcpInfo);

            return response;
        }

        [JsonProperty]
        public Event CollectionEvent { get; }

        [JsonProperty]
        public string Subscriber { get; }

        [JsonProperty]
        public Equipment Equipment { get; }

        private void Listener_DcpReceived(object sender, DcpListener.DcpReceivedEventArgs e)
        {
            if (e.Report.drsreport.report.eventId == CollectionEvent.Id)
            {
                TriggerReceived?.Invoke(this, EventArgs.Empty);
            }
        }

        [JsonProperty]
        public bool IsStartTrigger { get; }

        public event EventHandler TriggerReceived;
    }
}
