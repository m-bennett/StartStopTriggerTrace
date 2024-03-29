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
	/// Equipment
	/// </summary>
	[DataContract]
	[JsonObject(MemberSerialization.OptIn)]
	public partial class Equipment : IEquatable<Equipment>, IValidatableObject
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Equipment" /> class.
		/// </summary>
		/// <param name="name">name (required).</param>
		/// <param name="description">description.</param>
		/// <param name="equipmentModel">equipmentModel (required).</param>
		/// <param name="group">group (required).</param>
		/// <param name="orderInGroup">The order of this equipment inside the associated group. Indices start with 1, so first equipment will have order 1, second equipment will have order 2, and so on. Order need not be consecutive, so a group may have only 2 equipments with order 3 and 7 respectively. However, the order needs to be unique inside the group. (required).</param>
		/// <param name="connections">connections (required).</param>
		/// <param name="attributes">attributes.</param>
		/// <param name="factory">factory (required).</param>
		public Equipment(string id = default(string), string name = default(string), string description = default(string), EquipmentEquipmentModel equipmentModel = default(EquipmentEquipmentModel), EquipmentGroup group = default(EquipmentGroup), decimal? orderInGroup = default(decimal?), List<EquipmentConnection> connections = default(List<EquipmentConnection>), List<KeyValuePair> attributes = default(List<KeyValuePair>), EquipmentFactory factory = default(EquipmentFactory))
		{
			Name = name;
			EquipmentModel = equipmentModel;
			Group = group;
			OrderInGroup = orderInGroup;
			Connections = connections;
			Factory = factory;
			Description = description;
			Attributes = attributes;
			Id = id;
		}

		/// <summary>
		/// Gets or Sets Id
		/// </summary>
		[DataMember(Name = "id", EmitDefaultValue = false)]
		[JsonProperty]
		public string Id { get; private set; }

		/// <summary>
		/// Gets or Sets Name
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or Sets Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or Sets EquipmentModel
		/// </summary>
		public EquipmentEquipmentModel EquipmentModel { get; set; }

		/// <summary>
		/// Gets or Sets Group
		/// </summary>
		public EquipmentGroup Group { get; set; }

		/// <summary>
		/// The order of this equipment inside the associated group. Indices start with 1, so first equipment will have order 1, second equipment will have order 2, and so on. Order need not be consecutive, so a group may have only 2 equipments with order 3 and 7 respectively. However, the order needs to be unique inside the group.
		/// </summary>
		/// <value>The order of this equipment inside the associated group. Indices start with 1, so first equipment will have order 1, second equipment will have order 2, and so on. Order need not be consecutive, so a group may have only 2 equipments with order 3 and 7 respectively. However, the order needs to be unique inside the group.</value>
		public decimal? OrderInGroup { get; set; }

		/// <summary>
		/// Gets or Sets Connections
		/// </summary>
		[DataMember(Name = "connections", EmitDefaultValue = false)]
		[JsonProperty]
		public List<EquipmentConnection> Connections { get; set; }

		/// <summary>
		/// Gets or Sets Attributes
		/// </summary>
		public List<KeyValuePair> Attributes { get; set; }

		/// <summary>
		/// Gets or Sets SerialNumber
		/// </summary>
		public string SerialNumber { get; private set; }

		/// <summary>
		/// Gets or Sets Factory
		/// </summary>
		public EquipmentFactory Factory { get; set; }

		/// <summary>
		/// Returns the string presentation of the object
		/// </summary>
		/// <returns>String presentation of the object</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class Equipment {\r\n");
			sb.Append("  Id: ").Append(Id).Append("\r\n");
			sb.Append("  Name: ").Append(Name).Append("\r\n");
			sb.Append("  Description: ").Append(Description).Append("\r\n");
			sb.Append("  EquipmentModel: ").Append(EquipmentModel).Append("\r\n");
			sb.Append("  Group: ").Append(Group).Append("\r\n");
			sb.Append("  OrderInGroup: ").Append(OrderInGroup).Append("\r\n");
			sb.Append("  Connections: ").Append(Connections).Append("\r\n");
			sb.Append("  Attributes: ").Append(Attributes).Append("\r\n");
			sb.Append("  SerialNumber: ").Append(SerialNumber).Append("\r\n");
			sb.Append("  Factory: ").Append(Factory).Append("\r\n");
			sb.Append("}\r\n");
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
			return this.Equals(input as Equipment);
		}

		/// <summary>
		/// Returns true if Equipment instances are equal
		/// </summary>
		/// <param name="input">Instance of Equipment to be compared</param>
		/// <returns>Boolean</returns>
		public bool Equals(Equipment input)
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
					this.EquipmentModel == input.EquipmentModel ||
					(this.EquipmentModel != null &&
					this.EquipmentModel.Equals(input.EquipmentModel))
				) &&
				(
					this.Group == input.Group ||
					(this.Group != null &&
					this.Group.Equals(input.Group))
				) &&
				(
					this.OrderInGroup == input.OrderInGroup ||
					(this.OrderInGroup != null &&
					this.OrderInGroup.Equals(input.OrderInGroup))
				) &&
				(
					this.Connections == input.Connections ||
					this.Connections != null &&
					input.Connections != null &&
					this.Connections.SequenceEqual(input.Connections)
				) &&
				(
					this.Attributes == input.Attributes ||
					this.Attributes != null &&
					input.Attributes != null &&
					this.Attributes.SequenceEqual(input.Attributes)
				) &&
				(
					this.SerialNumber == input.SerialNumber ||
					(this.SerialNumber != null &&
					this.SerialNumber.Equals(input.SerialNumber))
				) &&
				(
					this.Factory == input.Factory ||
					(this.Factory != null &&
					this.Factory.Equals(input.Factory))
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
				if( this.EquipmentModel != null )
					hashCode = hashCode * 59 + this.EquipmentModel.GetHashCode();
				if( this.Group != null )
					hashCode = hashCode * 59 + this.Group.GetHashCode();
				if( this.OrderInGroup != null )
					hashCode = hashCode * 59 + this.OrderInGroup.GetHashCode();
				if( this.Connections != null )
					hashCode = hashCode * 59 + this.Connections.GetHashCode();
				if( this.Attributes != null )
					hashCode = hashCode * 59 + this.Attributes.GetHashCode();
				if( this.SerialNumber != null )
					hashCode = hashCode * 59 + this.SerialNumber.GetHashCode();
				if( this.Factory != null )
					hashCode = hashCode * 59 + this.Factory.GetHashCode();
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
