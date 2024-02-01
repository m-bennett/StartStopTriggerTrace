using Newtonsoft.Json;
using SapienceDcpManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StartStopTriggerTrace
{
    public class DcpListener : IDisposable
    {
        private HttpListener _listener;

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

        #region Singleton impementation
        private static readonly Lazy<DcpListener> Lazy = new Lazy<DcpListener>(() => new DcpListener());

        public static DcpListener Instance
        {
            get => Lazy.Value;
        }

        private DcpListener()
        {
        }
        #endregion

        public Task StartListening()
        {
            return Task.Run(() =>
            {
                if (_listener != null && _listener.IsListening)
                    return;  // Already listening

                StartHttpListener();
            });
        }

        public void StopListening()
        {
            if (_listener != null)
            {
                if (_listener.IsListening)
                    _listener.Stop();

                _listener = null;
            }
        }

        private void StartHttpListener()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(SapienceApiHandler.Instance.EndpointURL);

            try
            {
                _listener.Start();

                while (_listener != null &&
                       _listener.IsListening)
                {
                    var context = _listener.GetContext();

                    var request = context.Request;
                    var response = context.Response;
                    var requestData = GetRequestPostData(request);
                    var eventReport = JsonConvert.DeserializeObject<EventReport>(requestData);
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();

                    DcpReceived?.Invoke(this, new DcpReceivedEventArgs(eventReport));
                }
            }
            catch (Exception e)
            {
                Dispose();
                Console.WriteLine(e);
            }
        }

        public string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
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
