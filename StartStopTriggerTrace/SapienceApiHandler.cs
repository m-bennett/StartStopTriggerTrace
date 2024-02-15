using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using StartStopTriggerTrace.Models;
using System.Text;
using System.Web;

namespace StartStopTriggerTrace
{
	public class SapienceApiHandler
	{

		#region Constructors

		public SapienceApiHandler()
		{
			SapienceServer = ConfigurationManager.AppSettings.Get("SapienceServerUrl");

			httpClient = new HttpClient();
		}


		#endregion

		#region Types


		#endregion

		#region Fields

		private static SapienceApiHandler instance;
		public static SapienceApiHandler Instance
		{
			get
			{
				if( instance == null )
				{
					instance = new SapienceApiHandler();
				}

				return instance;
			}
		}

		private readonly HttpClient httpClient;

		private const ushort expirationSafetyMargin = 60;


		private DateTime expirationDate = default(DateTime);
		private DateTime refreshTokenExpirationDate = default(DateTime);
		private string accessToken = string.Empty;
		private string refreshToken = string.Empty;

		private readonly string tokenEndpoint = "/auth/realms/sapience-realm/protocol/openid-connect/token";


		public readonly string DCPRequestEndpoint = "/fcm/dcp";
		public readonly string FactoriesRequestEndpoint = "/fcm/factories";
		public readonly string EquipmentRequestEndpoint = "/fcm/equipments";
		public readonly string ConnectorDetailsRequestEndpoint = "/fcm/connectorDetails";
		public readonly string EquipmentModelEndpoint = "/fcm/equipmentModels";
		public readonly string CommunicationProtocolsEndpoint = "/fcm/communicationProtocols";
		public readonly string ConfigurationFileEndpoint = "/fcm/configurationFiles";
		public readonly string WebConnectorEquipmentEndpoint = "/webconnector/gem/equipment/";
		public readonly string RecipeSubscriberEndpoint = "/webconnector/gem/recipes/onRecipeNotification";
		public readonly string ExclusiveAccessRequestEndpoint = "/webconnector/equipment/exclusiveAccess";


		private readonly string SapienceServer = "http://localhost";

		private KeycloakToken token;

		private readonly string username = "sapience_admin";
		private readonly string password = "C!m3tR1x5@";
		private readonly string clientId = "sapience-client";
		private readonly string client_secret = "688ade80-f7b6-4e97-a82e-80198eb7e2cd";


		#endregion

		#region Properties


		#endregion

		#region Methods

		public string GetEquipmentUrl()
		{
			return $"{SapienceServer}{EquipmentRequestEndpoint}";
		}

		public string GetFactoriesUrl()
		{
			return $"{SapienceServer}{FactoriesRequestEndpoint}";
		}

		public string GetConnectorDetailsUrl()
		{
			return $"{SapienceServer}{ConnectorDetailsRequestEndpoint}";
		}

		public string GetEquipmentModelUrl()
		{
			return $"{SapienceServer}{EquipmentModelEndpoint}";
		}

		public string GetCommunicationProtocolsUrl()
		{
			return $"{SapienceServer}{CommunicationProtocolsEndpoint}";
		}
		public string GetRemoteCommandUrl(string equipmentId, string connectionId)
		{
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipmentId}/remoteCommand?connectorId={connectionId}";
		}
		public string GetDeleteRecipeUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/recipes/delete?connectorId={connectorId}";
		}
		public string GetGetRecipesUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/recipes?connectorId={connectorId}";
		}
		public string GetRecipeSubscribeUrl()
		{
			return $"{SapienceServer}{RecipeSubscriberEndpoint}";
		}
		public string GetRecipeDeleteSubscriptionUrl(string subscriptionId)
		{
			return $"{SapienceServer}{RecipeSubscriberEndpoint}/{subscriptionId}";
		}
		public string GetRecipeUrl(Equipment equipment, string ppid)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/recipes/{ppid}?connectorId={connectorId}";
		}
		public string GetSendRecipeUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/recipes/ascii?connectorId={connectorId}";
		}
		public string GetEquipmentConstantUrl(string equipmentId, string connectionId)
		{
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipmentId}/equipmentConstants/values?connectorId={connectionId}";
		}
		public string GetExclusiveAccessUrl()
		{
			return $"{SapienceServer}{ExclusiveAccessRequestEndpoint}";
		}
		public string GetCarrierActionRequestUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/carrierManagement/carrierActionRequest?connectorId={connectorId}";
		}
		public string GetPJCreateEnhRequestUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/processJobManagement/prJobCreateEnh?connectorId={connectorId}";
		}
		public string GetCreateObjectRequestUrl(Equipment equipment)
		{
			var connectorId = equipment.Connections[0].ConnectorDetail.Id;
			return $"{SapienceServer}{WebConnectorEquipmentEndpoint}{equipment.Id}/objectServices/createObjectRequest?connectorId={connectorId}";
		}
		



		public async Task<HttpResponseMessage> GetTask(string url)
		{
			try
			{
				await Authorize();
				SetHeaders();
				return await httpClient.GetAsync(url);
			}
			catch( Exception e )
			{
				Console.WriteLine(e);
				throw;
			}
		}


		private void SetHeaders()
		{
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<HttpResponseMessage> CreateDcp(string dcpRequest, string appkey)
		{
			await Authorize();
			var dcpContent = new StringContent(dcpRequest);
			dcpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			dcpContent.Headers.Add("X-App-Key", appkey);
			var response = await httpClient.PostAsync(GetDcpUrl(), dcpContent);

			return response;

		}

		public async Task<HttpResponseMessage> CreateFactory(string factoryCreateRequest)
		{
			await Authorize();
			var factoryContent = new StringContent(factoryCreateRequest);
			factoryContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await httpClient.PostAsync(GetFactoriesUrl(), factoryContent);
			return response;
		}

		public async Task<HttpResponseMessage> CreateEquipment(string equipmentCreateRequest)
		{
			await Authorize();
			var equipmentContent = new StringContent(equipmentCreateRequest);
			equipmentContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await httpClient.PostAsync(GetEquipmentUrl(), equipmentContent);
			return response;
		}

		public async Task<HttpResponseMessage> GetCommunicationProtocols()
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetCommunicationProtocolsUrl()}");
			return response;
		}

		public async Task<HttpResponseMessage> GetCommunicationProtocol(string id)
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetCommunicationProtocolsUrl()}/{id}?expand=communicationSettingRules,advancedSettingRules");
			return response;
		}

		public async Task<HttpResponseMessage> GetConnectorDetails()
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetConnectorDetailsUrl()}");
			return response;
		}

		public async Task<HttpResponseMessage> GetEquipmentModels()
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetEquipmentModelUrl()}?expand=equipmentConnectionTemplates,communicationProtocol");
			return response;
		}

		public async Task<HttpResponseMessage> GetEquipment()
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetEquipmentUrl()}?expand=connections,connections.communicationSettings,connections.advancedSettings,connections.equipmentConnectionTemplate,connections.connectorDetail,connections.connectorDetail.communicationProtocol,group");
			return response;
		}

		public async Task<HttpResponseMessage> GetFactories()
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetFactoriesUrl()}");
			return response;
		}

		public async Task<HttpResponseMessage> GetDcps(string appkey)
		{
			await Authorize();
			if( !string.IsNullOrEmpty(appkey))
				httpClient.DefaultRequestHeaders.Add("X-App-Key", appkey);

			var response = await httpClient.GetAsync($"{GetDcpUrl()}?expand=requestList%2CrequestList.parameterRequestsList%2CrequestList.startTriggers%2CrequestList.startTriggers.conditions%2CrequestList.stopTriggers%2CrequestList.stopTriggers.conditions%2Csubscribers%2C&sort=name&size=1000");

			if(httpClient.DefaultRequestHeaders.Contains("X-App-Key"))
				httpClient.DefaultRequestHeaders.Remove("X-App-Key");

			return response;
		}
		public async Task<string> GetToken()
		{
			await Authorize();
			return httpClient.DefaultRequestHeaders.Authorization.ToString().Substring(7);
		}

		public async Task<HttpResponseMessage> DeleteDcp(string dcpId)
		{
			await Authorize();
			var response = await httpClient.DeleteAsync($"{GetDcpUrl()}/{dcpId}");
			return response;

		}
		public async Task<HttpResponseMessage> GetConfigurationFile(string eqId)
		{
			await Authorize();
			var response = await httpClient.GetAsync($"{GetConfigFileUrl()}/{eqId}");
			return response;
		}

		public async Task<HttpResponseMessage> DeleteEquipment(string equipmentId)
		{
			await Authorize();
			var response = await httpClient.DeleteAsync($"{GetEquipmentUrl()}/{equipmentId}");
			return response;

		}
		public async Task<HttpResponseMessage> SendCommand(string equipmentId, string connectionId, RemoteCommand command)
		{
			await Authorize();
			var equipmentContent = new StringContent(JsonConvert.SerializeObject(command, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			equipmentContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var url = GetRemoteCommandUrl(equipmentId, connectionId);
			var response = await httpClient.PostAsync(url, equipmentContent);
			return response;
		}

		private async Task<HttpResponseMessage> SetParameter(string equipmentId, string connectionId, List<EquipmentConstant> ecList)
		{
			await Authorize();
			var json = JsonConvert.SerializeObject(ecList, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			var equipmentContent = new StringContent(JsonConvert.SerializeObject(ecList, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			equipmentContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var url = GetEquipmentConstantUrl(equipmentId, connectionId);
			var response = await httpClient.PutAsync(url, equipmentContent);
			return response;
		}

		public async Task<HttpResponseMessage> RequestExclusiveAccess()
		{
			await Authorize();
			var eaContent = new StringContent("");
			var url = GetExclusiveAccessUrl();
			var response = await httpClient.PutAsync(GetExclusiveAccessUrl(), eaContent);
			return response;
		}
		public async Task<HttpResponseMessage> RequestDeleteExclusiveAccess()
		{
			await Authorize();
			var response = await httpClient.DeleteAsync(GetExclusiveAccessUrl());
			return response;
		}




		public string GetDcpUrl()
		{
			return $"{SapienceServer}{DCPRequestEndpoint}";
		}
		public string GetConfigFileUrl()
		{
			return $"{SapienceServer}{ConfigurationFileEndpoint}";
		}


		private async Task Authorize()
		{
			var currentDate = DateTime.Now;

			//Test if the token's expirationDate has expired
			if( currentDate < refreshTokenExpirationDate )
			{
				//it hasn't, no need to get a new token or refresh the current token
				return;
			}

			//The token has expired, clear the httpClient's headers
			httpClient.DefaultRequestHeaders.Clear();

			//Test if we need to refresh the token or get a new token
			using( var formUrlEncodedContent = (currentDate < refreshTokenExpirationDate) ?
				new FormUrlEncodedContent(new[] {
				 new KeyValuePair<string, string>("client_id", clientId),
				 new KeyValuePair<string, string>("grant_type", "refresh_token"),
				 new KeyValuePair<string, string>("refresh_token", refreshToken)
				}) :
				new FormUrlEncodedContent(new[] {
				 new KeyValuePair<string, string>("client_id", clientId),
				 new KeyValuePair<string, string>("grant_type", "password"),
				 new KeyValuePair<string, string>("username", username),
				 new KeyValuePair<string, string>("password", password),
				 new KeyValuePair<string, string>("client_secret", client_secret)
				}) )
			{

				try
				{
					using( var response = await httpClient.PostAsync(GetKeycloakUri(), formUrlEncodedContent) )
					{
						response.EnsureSuccessStatusCode();
						var serializer = new DataContractJsonSerializer(typeof(KeycloakToken));
						using( var stream = await response.Content.ReadAsStreamAsync() )
						{
							token = ((KeycloakToken)serializer.ReadObject(stream));
						}

						//Set class field values from the token
						var paddedExpirationTime = token.expires_in - expirationSafetyMargin;
						expirationDate = currentDate.AddSeconds(paddedExpirationTime < 0 ? token.expires_in : paddedExpirationTime);

						var paddedRefreshExpirationTime = token.refresh_expires_in - expirationSafetyMargin;
						refreshTokenExpirationDate = currentDate.AddSeconds(paddedRefreshExpirationTime < 0 ? token.refresh_expires_in : paddedRefreshExpirationTime);

						accessToken = token.access_token;
						refreshToken = token.refresh_token;

						//Set the authorization header with the token
						httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
					}
				}
				catch( Exception e )
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}


		private string GetKeycloakUri()
		{
			return $"{SapienceServer}{tokenEndpoint}";
		}
		private string GetValuesUri(string eqId, List<string> parameters, string connectorId)
		{
			var query = HttpUtility.ParseQueryString("");
			query["variableIds"] = string.Join(",", parameters);
			query["connectorId"] = connectorId;
			return $"{SapienceServer}/webconnector/equipment/{eqId}/variableValues?{query}";
		}

		internal async Task<HttpResponseMessage> GetValues(List<Parameter> parameters, Equipment equipment)
		{
			await Authorize();
			var parameterIdList = new List<string>();
			foreach( Parameter p in parameters )
			{
				parameterIdList.Add(p.Id);
			}
			var response = await httpClient.GetAsync(GetValuesUri(equipment.Id, parameterIdList, equipment.Connections[0].ConnectorDetail.Id));
			return response;
		}
		internal async Task<HttpResponseMessage> CreateDcpFromManager(DcpInfo dcpInfo)
		{
			var dcp = new DataCollectionPlan()
			{
				Name = dcpInfo.DcpName,
				Description = dcpInfo.Description,
				KafkaTopic = dcpInfo.KafkaTopic,
				Subscribers = new List<Subscriber>() { new Subscriber(dcpInfo.Subscriber) }
			};
			dcp.Equipment = new Equipment(dcpInfo.Equipment.Id);
			dcp.EquipmentConnection = new EquipmentConnection(dcpInfo.Equipment.Connections[0].Id);

			var request = new Request();
			if( dcpInfo.RequestType == RequestType.Trace )
			{
				request.TraceId = dcpInfo.Id;
				request.RequestType = RequestType.Trace;
				request.CollectionIntervalMs = Convert.ToInt32(dcpInfo.Interval);
				request.GroupSize = dcpInfo.GroupSize;
				request.CollectionCount = dcpInfo.CollectionCount;
				request.IsCyclical = dcpInfo.IsCyclical;
				request.StartTriggers = dcpInfo.StartTriggers;
				request.StopTriggers = dcpInfo.StopTriggers;
				foreach( Parameter param in dcpInfo.Parameters )
				{
					request.ParameterRequestsList.Add(new ParameterRequest(param.Id));
				}
			}
			else if( dcpInfo.RequestType == RequestType.Event )
			{
				request.EventId = dcpInfo.EventId;
				request.RequestType = RequestType.Event;
				request.CollectionIntervalMs = null;
				request.CollectionCount = null;
				request.IsCyclical = null;
				request.GroupSize = null;
				request.ParameterRequestsList = null;
			}
			else if( dcpInfo.RequestType == RequestType.Alarm )
			{
				request.AlarmId = dcpInfo.AlarmId;
				request.RequestType = RequestType.Alarm;
				request.Severity = dcpInfo.Severity == "Error" ? "ERROR" : "WARNING";
				request.CollectionIntervalMs = null;
				request.CollectionCount = null;
				request.IsCyclical = null;
				request.GroupSize = null;
				request.ParameterRequestsList = null;
			}

			dcp.RequestList.Add(request);

			var jsonstring = JsonConvert.SerializeObject(dcp, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			await Authorize();
			var json = JsonConvert.SerializeObject(dcp, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
			var dcpContent = new StringContent(JsonConvert.SerializeObject(dcp, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			dcpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			dcpContent.Headers.Add("X-App-Key", dcpInfo.AppKey);
			var response = await httpClient.PostAsync(GetDcpUrl(), dcpContent);

			return response;
		}

		public async Task<HttpResponseMessage> SendRemoteCommand(Equipment equipment, RemoteCommand command)
		{
			await GetExclusiveAccesss();
			var commandResponse = await Instance.SendCommand(equipment.Id, equipment.Connections[0].ConnectorDetail.Id, command);
			await DeleteExclusiveAccess();
			return commandResponse;
		}

		public async Task<HttpResponseMessage> GetExclusiveAccesss()
		{
			return await Instance.RequestExclusiveAccess();
		}
		public async Task<HttpResponseMessage> DeleteExclusiveAccess()
		{
			return await Instance.RequestDeleteExclusiveAccess();
		}

		public async Task<HttpResponseMessage> SetParameter(Equipment equipment, List<EquipmentConstant> ecList)
		{
			await GetExclusiveAccesss();
			var response = await Instance.SetParameter(equipment.Id, equipment.Connections[0].ConnectorDetail.Id, ecList);
			await DeleteExclusiveAccess();
			return response;
		}

		public async Task<HttpResponseMessage> GetRecipe(Equipment equipment, string recipeName)
		{
			await Authorize();
			var url = GetRecipeUrl(equipment, recipeName);
			var response = await httpClient.GetAsync(url);
			return response;
		}

		public async Task<HttpResponseMessage> SendRecipe(Equipment equipment, string recipePath)
		{
			await Authorize();
			using( var multipartFormContent = new MultipartFormDataContent() )
			{
				var url = GetSendRecipeUrl(equipment);

				//Load the file and set the file's Content-Type header
				var fileStreamContent = new StreamContent(File.OpenRead(recipePath));
				fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

				//Add the file
				multipartFormContent.Add(fileStreamContent, "file", recipePath.Substring(recipePath.LastIndexOf('\\') + 1));

				//Send it
				await GetExclusiveAccesss();
				var response = await httpClient.PostAsync(url, multipartFormContent);
				await DeleteExclusiveAccess();
				return response;
			}
		}

		internal async Task<HttpResponseMessage> DeleteRecipe(Equipment equipment, string recipeName)
		{
			await Authorize();
			var url = GetDeleteRecipeUrl(equipment);
			await GetExclusiveAccesss();
			var recipeContent = new StringContent(JsonConvert.SerializeObject(new List<string>() { recipeName }, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			recipeContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await httpClient.PostAsync(url, recipeContent);
			await DeleteExclusiveAccess();
			return response;
		}

		public async Task<HttpResponseMessage> GetEquipmentRecipes(Equipment equipment)
		{
			await Authorize();
			var url = GetGetRecipesUrl(equipment);
			var response = await httpClient.GetAsync(url);
			return response;
		}

		public async Task<HttpResponseMessage> EquipmentRecipeNotificationsUnSubscribe(string subscriptionId)
		{
			await Authorize();
			var url = GetRecipeDeleteSubscriptionUrl(subscriptionId);
			await GetExclusiveAccesss();
			var response = await httpClient.DeleteAsync(url);
			await DeleteExclusiveAccess();
			return response;
		}

		public async Task<HttpResponseMessage> CarrierActionRequest(Equipment equipment, string carrierAction, string carrierId)
		{
			var car = new CarrierActionRequest()
			{
				carrierAction = carrierAction,
				carrierId = carrierId,
				ptn = 1,
				carrierAttributes = new List<List<CarrierAttribute>>()
			};
			var carrierActionContent = new StringContent(JsonConvert.SerializeObject(car, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			carrierActionContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			await Authorize();
			var url = GetCarrierActionRequestUrl(equipment);
			await GetExclusiveAccesss();
			var response = await httpClient.PutAsync(url, carrierActionContent);
			await DeleteExclusiveAccess();
			return response;
		}

		internal async Task<HttpResponseMessage> PrJobCreateEnh(Equipment equipment, string carrierId, string recipeName, string pjid, List<int> slotList)
		{
			var pjRequest = new prJobCreateEnhRequest()
			{
				dataId = 25,
				prJobId = pjid,
				mf = "Carriers",
				prProcessStart = true,
				prPauseEvent = new List<int>(),
				carrierSlotList = new List<CarrierSlotList>()
				{
					{new CarrierSlotList{carrierId = carrierId, slotIds = slotList } }
				},
				processJobRecipe = new ProcessJobRecipe
				{
					recipe = recipeName,
					prRecipeMethod = 0,
					recipeParameters = new List<RecipeParameter> { }
				}
			};

			var pj = new StringContent(JsonConvert.SerializeObject(pjRequest, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			pj.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			await Authorize();
			var url = GetPJCreateEnhRequestUrl(equipment);
			await GetExclusiveAccesss();
			var response = await httpClient.PostAsync(url, pj);
			await DeleteExclusiveAccess();
			return response;
		}

		internal async Task<HttpResponseMessage> CreateObjectRequest(Equipment equipment, string controlJobId, string carrierId, string prjobId)
		{
			var cjRequst = new CreateObjectRequest()
			{
				objspec = "ControlJob",
				objtype = "ControlJob",
				attributeRequests = new List<AttributeRequest>
				{
					new AttributeRequest{attrid = "ObjID", attrdata = new Attrdata{ data = controlJobId, dataType = "STRING" }},
					new AttributeRequest{attrid = "MtrlOutSpec", attrdata = new Attrdata{ dataType = "LIST", listData = new List<Attrdata>()}},
					new AttributeRequest{attrid = "StartMethod", attrdata = new Attrdata{data = true, dataType = "BOOL" }},
					new AttributeRequest{attrid = "ProcessOrderMgmt", attrdata = new Attrdata{ data = 3, dataType = "UINT8" }},
					new AttributeRequest
					{
						attrid = "CarrierInputSpec", attrdata = new Attrdata
						{
							dataType = "LIST", listData = new List<Attrdata>
							{
								{
									new Attrdata{data = carrierId, dataType = "STRING"}
								}
							}
						}
					},
					new AttributeRequest
					{
						attrid = "ProcessingCtrlSpec", attrdata = new Attrdata
						{
							dataType = "LIST", listData = new List<Attrdata>
							{
								{
									new Attrdata
									{
										dataType = "LIST", listData = new List<Attrdata>
										{
											{new Attrdata{data = prjobId, dataType = "STRING"}},
											{new Attrdata{dataType = "LIST", listData = new List<Attrdata>()} },
											{new Attrdata{dataType = "LIST", listData = new List<Attrdata>()} }
										}
									}
								}
							}
						}
					}
				}
			};
			var cj = new StringContent(JsonConvert.SerializeObject(cjRequst, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
			cj.Headers.ContentType = new MediaTypeHeaderValue("application/json");

			await Authorize();
			var url = GetCreateObjectRequestUrl(equipment);
			await GetExclusiveAccesss();
			var response = await httpClient.PutAsync(url, cj);
			await DeleteExclusiveAccess();
			return response;
		}




		#endregion

	}
}
