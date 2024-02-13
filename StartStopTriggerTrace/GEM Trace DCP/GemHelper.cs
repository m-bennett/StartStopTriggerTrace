using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;


using Newtonsoft.Json;
using StartStopTriggerTrace.Models;

namespace StartStopTriggerTrace.GEM_Trace_DCP
{
    public class GemHelper
    {
        public static async Task<string> CheckStatusAndGetDcpId(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return "";
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CreateResponse>(jsonString).Id;
        }

        public class DataLists
        {
            public List<Parameter> ParameterList { get; set; }
            public List<Event> EventList { get; set; }
        }
        public static async Task<DataLists> GetEquipmentInfosAsync(string configFileId)
        {
            var data = new DataLists();
            var response = await SapienceApiHandler.Instance.GetConfigurationFile(configFileId);
            if (response.IsSuccessStatusCode)
            {
                var xml = response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.Result);

                ParseGemData(doc, data);
                return data;
            }
            else
            {
                Log.Instance.WriteLog($"Error getting equipment config from Sapience. {response.Content.ReadAsStringAsync()}");
                return null;
            }
        }

        private string GetElementSourceId(XmlElement element)
        {
            var parentNode = (XmlElement)element.ParentNode;
            if (parentNode.HasAttribute("SourceID"))
            {
                return parentNode.GetAttribute("SourceID");
            }
            else
            {
                return GetElementSourceId(parentNode);
            }
        }

        private static void ParseGemData(XmlDocument doc, DataLists data)
        {
            data.ParameterList = new List<Parameter>();
            data.EventList = new List<Event>();

            var StatusVariableList = doc.SelectNodes("//StatusVariables");
            foreach (XmlElement list in StatusVariableList)
            {
                var variableLists = list.ChildNodes;
                foreach (XmlElement parameter in variableLists)
                {
                    var name = parameter.SelectSingleNode("Name")?.InnerText;
                    var sourceId = parameter.SelectSingleNode("Id").InnerText;
                    var param = new Parameter(name, $"{sourceId}");
                    data.ParameterList.Add(param);
                }
            }

            var EcVariableLists = doc.SelectNodes("//EquipmentConstants");
            foreach (XmlElement list in EcVariableLists)
            {
                var variableDescriptions = list.SelectNodes("//EquipmentConstantDefinition");
                foreach (XmlElement parameter in variableDescriptions)
                {
                    var name = parameter.SelectSingleNode("Name")?.InnerText;
                    var sourceId = parameter.SelectSingleNode("Id").InnerText;
                    var param = new Parameter(name, $"{sourceId}");
                    data.ParameterList.Add(param);
                }
            }
            data.ParameterList = data.ParameterList.OrderBy(x => x.Name).ToList();


            var eventLists = doc.SelectNodes("//CollectionEvents");
            foreach (XmlElement list in eventLists)
            {
                var eventDescription = list.SelectNodes("//CollectionEventDescription");
                foreach (XmlElement ev in eventDescription)
                {
                    var name = ev.SelectSingleNode("Name")?.InnerText;
                    var sourceId = ev.SelectSingleNode("Id").InnerText;
                    var collectionEvent = new Event(name, $"{sourceId}");
                    data.EventList.Add(collectionEvent);
                }
            }
            data.EventList = data.EventList.OrderBy(x => x.Name).ToList();
        }


    }
}
