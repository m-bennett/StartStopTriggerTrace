using StartStopTriggerTrace.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Configuration;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{

    [JsonObject(MemberSerialization.OptIn)]
    public class GemTraceDcpWithTriggers
    {
        public enum DcpStatus
        {
            Stopped,
            Started
        }

        public event EventHandler StatusChanged;
        private string appkey = ConfigurationManager.AppSettings.Get("AppKey");

        public GemTraceDcpWithTriggers(string id,
                                       Equipment equipment,
                                       IEnumerable<Parameter> parameters,
                                       string kafkaTopic,
                                       string description,
                                       IEnumerable<GemTraceDcpTrigger> triggers,
                                       string interval)
        {
            Id = id;
            Equipment = equipment;
            Parameters = parameters.ToList();
            KafkaTopic = kafkaTopic;
            Description = $"{description}";
            Triggers = triggers.ToList();
            Interval = interval;

            foreach (var trigger in Triggers)
            {
                trigger.TriggerReceived += Trigger_TriggerReceived;
            }
        }

        public async Task Start()
        {
            foreach (var trigger in Triggers)
            {
                var response = await trigger.CreateDcpFromManager();
                var jsonString = await response.Content.ReadAsStringAsync();
            }
        }

        public async Task Delete()
        {
            var response = await SapienceApiHandler.Instance.GetDcps(appkey);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var dcpList = JsonConvert.DeserializeObject<DataCollectionPlanResponse>(jsonString);

                foreach (var dcp in dcpList.Content)
                {
                    if (dcp.Description.Contains(Id))
                    {
                        response = await SapienceApiHandler.Instance.DeleteDcp(dcp.Id);
                    }
                }
            }
            foreach(var trigger in Triggers)
            {
                trigger.TriggerReceived -= Trigger_TriggerReceived;
                trigger.Delete();
            }
            Triggers.Clear();

        }

        private async void Trigger_TriggerReceived(object sender, EventArgs e)
        {
            var trigger = (GemTraceDcpTrigger)sender;

            if (trigger.IsStartTrigger && Status == DcpStatus.Stopped)
            {
                var description = $"StartStopTriggerApp_Trace_DCP_{Id}";
                // Start trace
                var dcpInfo = new DcpInfo()
                {
                    DcpName = description,
                    Description = description,
                    Id = Id,
                    RequestType = RequestType.Trace,
                    Interval = Interval,
                    CollectionCount = 0,
                    GroupSize = 1,
                    Subscriber = "localhost",
                    KafkaTopic = KafkaTopic,
                    Equipment = Equipment,
                    Parameters = Parameters
                };

                var response = await SapienceApiHandler.Instance.CreateDcpFromManager(dcpInfo);
                if (!response.IsSuccessStatusCode)
                    return;
                var jsonString = await response.Content.ReadAsStringAsync();
                TraceDcpId = JsonConvert.DeserializeObject<CreateResponse>(jsonString).Id;

                Status = DcpStatus.Started;
            }
            else if (trigger.IsStartTrigger == false && Status == DcpStatus.Started)
            {
                var response = await SapienceApiHandler.Instance.DeleteDcp(TraceDcpId);
                if (!response.IsSuccessStatusCode)
                    return;

                TraceDcpId = "";
                Status = DcpStatus.Stopped;
            }
        }

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        private string TraceDcpId { get; set; }

        [JsonProperty]
        public Equipment Equipment { get; }

        [JsonProperty]
        public List<Parameter> Parameters { get; }

        [JsonProperty]
        public string KafkaTopic { get; }

        [JsonProperty]
        public string Description { get; set; }

        private DcpStatus _status = DcpStatus.Stopped;
        public DcpStatus Status
        {
            get
            {
                lock (this)
                    return _status;

            }
            private set
            {
                lock (this)
                {
                    if (value == _status)
                        return;  // No change

                    _status = value;

                    StatusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [JsonProperty]
        public List<GemTraceDcpTrigger> Triggers { get; }

        [JsonProperty]
        public string Interval { get; }
    }
}
