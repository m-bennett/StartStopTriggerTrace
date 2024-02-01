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

        public GemTraceDcpWithTriggers(Equipment equipment,
                                       IEnumerable<Parameter> parameters,
                                       string description,
                                       IEnumerable<GemTraceDcpTrigger> triggers,
                                       string interval,
                                       string subscriber)
        {
            Equipment = equipment;
            Parameters = parameters.ToList();
            Description = description;
            Triggers = triggers.ToList();
            Interval = interval;
            Subscriber = subscriber;
        }

        public async Task Start()
        {
            foreach (var trigger in Triggers)
            {
                trigger.TriggerReceived += Trigger_TriggerReceived;
                var response = await trigger.CreateDcpFromManager();
                var jsonString = await response.Content.ReadAsStringAsync();
            }
        }

        private async void Trigger_TriggerReceived(object sender, EventArgs e)
        {
            var trigger = (GemTraceDcpTrigger)sender;
            var description = $"Start Stop trigger app Trace DCP {Guid.NewGuid()}";

            if (trigger.TriggerType == GemTraceDcpTrigger.TriggerTypes.Start &&
                Status == DcpStatus.Stopped)
            {
                // Start trace
                var dcpInfo = new DcpInfo()
                {
                    DcpName = description,
                    Description = description,
                    Id = $"{new Random().Next(0, 10000)}",
                    RequestType = RequestType.Trace,
                    Interval = Interval,
                    CollectionCount = 0,
                    GroupSize = 1,
                    Subscriber = Subscriber,
                    Equipment = Equipment,
                    Parameters = Parameters
                };

                var response = await SapienceApiHandler.Instance.CreateDcpFromManager(dcpInfo);
                var jsonString = await response.Content.ReadAsStringAsync();
                TraceDcpId = JsonConvert.DeserializeObject<CreateResponse>(jsonString).Id;

            Status = DcpStatus.Started;
            }
            else if (trigger.TriggerType == GemTraceDcpTrigger.TriggerTypes.Stop &&
                Status == DcpStatus.Started)
            {

                await SapienceApiHandler.Instance.DeleteDcp(TraceDcpId);

                TraceDcpId = "";
                Status = DcpStatus.Stopped;
            }
        }

        [JsonProperty]
        private string TraceDcpId { get; set; }

        [JsonProperty]
        public Equipment Equipment { get; }

        [JsonProperty]
        public List<Parameter> Parameters { get; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public string Subscriber { get; }

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
