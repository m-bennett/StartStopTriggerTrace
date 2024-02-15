using Newtonsoft.Json;
using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using StartStopTriggerTrace.Extensions;
using System.Configuration;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GemTraceDcpTrigger
    {
        private DcpInfo _dcpInfo;
        private KafkaConsumer _consumer = new KafkaConsumer();

        public GemTraceDcpTrigger()
        {
        }
        public GemTraceDcpTrigger(TriggerInfo ti)
        {
            var parameters = new List<Parameter>();
            IsStartTrigger = ti.IsStartTrigger;
            CollectionEvent = ti.CollectionEvent;
            Subscriber = ti.Subscriber;
            KafkaTopic = ti.KafkaTopic;
            Equipment = ti.Equipment;

            _dcpInfo = new DcpInfo();
        }

        public async Task<HttpResponseMessage> CreateDcpFromManager()
        {
            _consumer.DcpReceived += Listener_DcpReceived;
            _consumer.KafkaServer = ConfigurationManager.AppSettings.Get("KafkaServer");

            var guid = Guid.NewGuid().ToString();
            _consumer.GroupId = guid;
            _consumer.Topic = KafkaTopic;

            var parameters = new List<Parameter>();
            _dcpInfo = new DcpInfo();

            _dcpInfo.DcpName = $"{(IsStartTrigger ? "Start" : "Stop")}TriggerEvent_EventId_{CollectionEvent.Id}_{guid}";
            _dcpInfo.EventId = CollectionEvent.Id;
            _dcpInfo.Description = Description;
            _dcpInfo.Subscriber = Subscriber;
            _dcpInfo.Parameters = parameters;
            _dcpInfo.KafkaTopic = KafkaTopic;
            _dcpInfo.RequestType = RequestType.Event;
            _dcpInfo.Equipment = Equipment;

            var response = await SapienceApiHandler.Instance.CreateDcpFromManager(_dcpInfo);
            _dcpInfo.Id = await GemHelper.CheckStatusAndGetDcpId(response);
            if (String.IsNullOrEmpty(_dcpInfo.Id))
                Log.Instance.WriteLog($"Error creating Sapience DCP for {CollectionEvent.Id}. {response.Content.ReadAsStringAsync()}");
            else
                Log.Instance.WriteLog($"Sapience DCP {_dcpInfo.Id} created successfully for event ID = {_dcpInfo.EventId}.");

            _consumer.DcpInfo = _dcpInfo;
            _consumer.StartListening();

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
            _consumer.StopListening();
            _consumer.DcpReceived -= Listener_DcpReceived;
        }

        [JsonProperty]
        public Event CollectionEvent { get; set; }

        [JsonProperty]
        public string Subscriber { get; set; }

        [JsonProperty]
        public string KafkaTopic { get; set; }

        [JsonProperty]
        public Equipment Equipment { get; set; }

        private void Listener_DcpReceived(object sender, KafkaConsumer.DcpReceivedEventArgs e)
        {
            if (e.Report.drsreport.report.eventId == CollectionEvent.Id)
            {
                TriggerReceived?.Invoke(this, EventArgs.Empty);
                Log.Instance.WriteLog($"{(IsStartTrigger ? "Start" : "Stop")} trigger event Id = {CollectionEvent.Id} received.");
            }
        }

        [JsonProperty]
        public bool IsStartTrigger { get; set; }

        public event EventHandler TriggerReceived;
    }
}
