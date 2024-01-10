/* 
 * Factory Configuration Manager
 *
 * These are the specifications for Sapience Factory Configuration Manager Web API.  ### Schema Notes  1. required attributes (marked with a *) must be provided in POST or PUT request body and returned in the server response.  2. readOnly attributes will be automatically added to entities by the server. They will be a part of GET response. However, they should not be a part of POST or PUT request. If present in request body, they will be ignored by the server. The only exception to this is the id attribute of all entities. It is marked as readOnly, but must be added in PUT request body, so that the server can identify the entity. ### Association Pattern instead of Creation Pattern  It must be noted that for all POST and PUT endpoints, we follow a standard pattern where child entities are associated and not created. For example, all parent-child relationships should look like this:  ``` Factory:  { // this is the parent entity   \"id\": \"uuid\"   // more fields here   \"groups\": [{ // this is the child entity     \"id\": \"uuid\"     // no more fields here   }] } ```  This pattern requires us to create all child entities first (e.g. POST /fcm/groups) and then associate them with their corresponding parent using POST or PUT request(e.g. POST /fcm/factories or PUT /fcm/factories) as mentioned below:  ``` POST /fcm/groups // send >>> {   // required fields here } // returns the id >>> \"1234-123456\"   POST /fcm/factories // or can be PUT // send >>> {   // some fields here   \"groups\": [{ // we associate it to a group     \"id\": \"1234-123456\"   }] } ```  ### Automatic Bi-directional Mapping  Most of the entities have a bi-directional reference with their children. Relationship should be established from one entity to another entity and the server takes care of establishing bi-directional relationship internally. For example:  ``` Factory:  {   // some fields here   \"groups\": [] // reference to Groups }  Group:  {   // some fields here   \"factory\": {} // reference to Factory } ```  In such cases, you only need to set the relationship from one side and other side will be internaly set by the server. For example:  ``` GET /fcm/groups/1234-123456 // returns >>> {   // some fields here   \"factory\": null // currently not assigned to a factory }   PUT /fcm/factories/5678-567890 // send >>> {   // some fields here   \"groups\": [{ // we associate it to a group     \"id\": \"1234-123456\"   }] }   GET /fcm/groups/1234-123456 // returns >>> {   // some fields here   \"factory\": { // automatically got associated to the factory     \"id\": \"5678-567890\"   } } ```  ### References  - **Open API Specification** [https://swagger.io/specification/](https://swagger.io/specification/)  © Cimetrix 2020 
 *
 * OpenAPI spec version: 0.3.7
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StartStopTriggerTrace.Models
{
	/// <summary>
	/// EquipmentConnectionTemplate
	/// </summary>
	[DataContract]
	public partial class EquipmentConnectionTemplate : IEquatable<EquipmentConnectionTemplate>, IValidatableObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EquipmentConnectionTemplate" /> class.
		/// </summary>
		/// <param name="name">name (required).</param>
		/// <param name="description">description.</param>
		/// <param name="communicationProtocol">communicationProtocol (required).</param>
		/// <param name="protocolCapabilities">protocolCapabilities (required).</param>
		/// <param name="configurationFile">configurationFile (required).</param>
		public EquipmentConnectionTemplate(string name = default(string), string description = default(string), CommunicationProtocol communicationProtocol = default(CommunicationProtocol), List<KeyValuePair> protocolCapabilities = default(List<KeyValuePair>), ConfigurationFile configurationFile = default(ConfigurationFile))
		{
			this.Name = name;
			this.CommunicationProtocol = communicationProtocol;
			this.ProtocolCapabilities = protocolCapabilities;
			this.ConfigurationFile = configurationFile;
		}

		/// <summary>
		/// Gets or Sets Id
		/// </summary>
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Id { get; private set; }

		/// <summary>
		/// Gets or Sets Name
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets Description
		/// </summary>
		[DataMember(Name = "description", EmitDefaultValue = false)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or Sets CommunicationProtocol
		/// </summary>
		[DataMember(Name = "communicationProtocol", EmitDefaultValue = false)]
		public CommunicationProtocol CommunicationProtocol { get; set; }

		/// <summary>
		/// Gets or Sets ProtocolCapabilities
		/// </summary>
		[DataMember(Name = "protocolCapabilities", EmitDefaultValue = false)]
		public List<KeyValuePair> ProtocolCapabilities { get; set; }

		/// <summary>
		/// Gets or Sets ConfigurationFile
		/// </summary>
		[DataMember(Name = "configurationFile", EmitDefaultValue = false)]
		public ConfigurationFile ConfigurationFile { get; set; }

		/// <summary>
		/// Returns the string presentation of the object
		/// </summary>
		/// <returns>String presentation of the object</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class EquipmentConnectionTemplate {\n");
			sb.Append("  Id: ").Append(Id).Append("\n");
			sb.Append("  Name: ").Append(Name).Append("\n");
			sb.Append("  Description: ").Append(Description).Append("\n");
			sb.Append("  CommunicationProtocol: ").Append(CommunicationProtocol).Append("\n");
			sb.Append("  ProtocolCapabilities: ").Append(ProtocolCapabilities).Append("\n");
			sb.Append("  ConfigurationFile: ").Append(ConfigurationFile).Append("\n");
			sb.Append("}\n");
			return sb.ToString();
		}

		/// <summary>
		/// Returns the JSON string presentation of the object
		/// </summary>
		/// <returns>JSON string presentation of the object</returns>
		public virtual string ToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		/// <summary>
		/// Returns true if objects are equal
		/// </summary>
		/// <param name="input">Object to be compared</param>
		/// <returns>Boolean</returns>
		public override bool Equals(object input)
		{
			return this.Equals(input as EquipmentConnectionTemplate);
		}

		/// <summary>
		/// Returns true if EquipmentConnectionTemplate instances are equal
		/// </summary>
		/// <param name="input">Instance of EquipmentConnectionTemplate to be compared</param>
		/// <returns>Boolean</returns>
		public bool Equals(EquipmentConnectionTemplate input)
		{
			if( input == null )
				return false;

			return
				(
					this.Id == input.Id ||
					(this.Id != null &&
					this.Id.Equals(input.Id))
				) &&
				(
					this.Name == input.Name ||
					(this.Name != null &&
					this.Name.Equals(input.Name))
				) &&
				(
					this.Description == input.Description ||
					(this.Description != null &&
					this.Description.Equals(input.Description))
				) &&
				(
					this.CommunicationProtocol == input.CommunicationProtocol ||
					(this.CommunicationProtocol != null &&
					this.CommunicationProtocol.Equals(input.CommunicationProtocol))
				) &&
				(
					this.ProtocolCapabilities == input.ProtocolCapabilities ||
					this.ProtocolCapabilities != null &&
					input.ProtocolCapabilities != null &&
					this.ProtocolCapabilities.SequenceEqual(input.ProtocolCapabilities)
				) &&
				(
					this.ConfigurationFile == input.ConfigurationFile ||
					(this.ConfigurationFile != null &&
					this.ConfigurationFile.Equals(input.ConfigurationFile))
				);
		}

		/// <summary>
		/// Gets the hash code
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				int hashCode = 41;
				if( this.Id != null )
					hashCode = hashCode * 59 + this.Id.GetHashCode();
				if( this.Name != null )
					hashCode = hashCode * 59 + this.Name.GetHashCode();
				if( this.Description != null )
					hashCode = hashCode * 59 + this.Description.GetHashCode();
				if( this.CommunicationProtocol != null )
					hashCode = hashCode * 59 + this.CommunicationProtocol.GetHashCode();
				if( this.ProtocolCapabilities != null )
					hashCode = hashCode * 59 + this.ProtocolCapabilities.GetHashCode();
				if( this.ConfigurationFile != null )
					hashCode = hashCode * 59 + this.ConfigurationFile.GetHashCode();
				return hashCode;
			}
		}

		/// <summary>
		/// To validate all properties of the instance
		/// </summary>
		/// <param name="validationContext">Validation context</param>
		/// <returns>Validation Result</returns>
		IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
		{
			yield break;
		}
	}
}
