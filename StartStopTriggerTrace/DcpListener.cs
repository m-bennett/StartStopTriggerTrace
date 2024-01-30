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
    public class DcpListener
    {
        private HttpListener listener;

        public class DcpReceivedEventArgs : EventArgs
        {
            public DcpReceivedEventArgs(EventReport report)
            {
                Report = report;
            }

            public EventReport Report { get; }
        }

        public event EventHandler<DcpReceivedEventArgs> DcpReceived;

        private static DcpListener _dcpListener;

        public static DcpListener Instance
        {
            get
            {
                if (_dcpListener == null)
                    _dcpListener = new DcpListener();

                return _dcpListener;
            }
        }

        public DcpListener()
        {
            Task.Run(StartHttpListener);
        }

        private void StartHttpListener()
        {
            listener = new HttpListener();
            listener.Prefixes.Add(SapienceApiHandler.Instance.EndpointURL);

            try
            {
                listener.Start();

                while (true)
                {
                    var context = listener.GetContext();

                    var request = context.Request;
                    var response = context.Response;
                    var requestData = GetRequestPostData(request);
                    var eventReport = JsonConvert.DeserializeObject<EventReport>(requestData);
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Close();

                    DcpReceived.Invoke(eventReport);
                }
            }
            catch (Exception e)
            {
                listener.Stop();
                listener = null;

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
    }
}
