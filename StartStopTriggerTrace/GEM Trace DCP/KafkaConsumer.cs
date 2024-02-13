using Newtonsoft.Json;
using SapienceDcpManager.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json.Linq;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    public class KafkaConsumer : IDisposable
    {
        private IConsumer<Ignore, string> _consumer;
        private CancellationTokenSource _cts = new CancellationTokenSource();

        #region .NET events
        public class DcpReceivedEventArgs : EventArgs
        {
            public DcpReceivedEventArgs(EventReport report)
            {
                Report = report;
            }

            public EventReport Report { get; }
        }

        public event EventHandler<DcpReceivedEventArgs> DcpReceived;
        #endregion

        public string KafkaServer { get; set; }
        public string GroupId { get; set; }
        public string Topic { get; set; }
        public DcpInfo DcpInfo { get; set; }

        public KafkaConsumer()
        {
        }

        public void StartListening()
        {
            Task.Run(() =>
            {
                if (_consumer != null)
                    return;  // Already

                StartConsumer(_cts.Token);
            });
        }

        public void StopListening()
        {
            if (_consumer != null)
            {
                _cts.Cancel();
            }
        }

        private void StartConsumer(CancellationToken ct)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = KafkaServer,
                GroupId = GroupId
            };

            using (_consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                _consumer.Subscribe(new string[] { Topic });

                Log.Instance.WriteLog($"Starting consumer for event ID = {DcpInfo.EventId} on Topic {DcpInfo.KafkaTopic}.");

                try
                {
                    while (!_cts.IsCancellationRequested && _consumer != null)
                    {
                        var consumeResult = _consumer.Consume(ct);

                        KafkaMessageReceived(consumeResult.Message.Value);
                    }
                }
                catch(OperationCanceledException)
                {
                    Log.Instance.WriteLog($"Consumer for DCP ID = {DcpInfo.Id} closed.");
                }
                catch (Exception ex)
                {
                    Log.Instance.WriteLog($"StartConsumer() for {GroupId}. {ex.Message}");
                }

                _consumer.Close();
            }
        }

        private void KafkaMessageReceived(string message)
        {
            var jsonObject = JValue.Parse(message);
            var drsreport = JValue.Parse(jsonObject["drsreport"].ToString());
            var id = drsreport["dataCollectionPlanId"].ToString();

            if (id == DcpInfo.Id)
            {
                var report = JsonConvert.DeserializeObject<EventReport>(message);
                DcpReceived?.Invoke(this, new DcpReceivedEventArgs(report));
            }
        }


        #region IDispose implementation
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources

                    StopListening();
                    DcpReceived = null;
                }

                // Dispose unmanaged resources

                _disposed = true;
            }
        }
        #endregion
    }
}
