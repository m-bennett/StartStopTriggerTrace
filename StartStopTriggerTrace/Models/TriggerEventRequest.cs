﻿/* 
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
    /// TriggerEventRequest
    /// </summary>
    [DataContract]
    public partial class TriggerEventRequest : IEquatable<TriggerEventRequest>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TriggerEventRequest" /> class.
        /// </summary>
        /// <param name="triggerRequestType">TriggerEventRequest.</param>
        /// <param name="eventId">Required/Used for EDA Trace Request.</param>
        /// <param name="semiObjType">semiObjType.</param>
        /// <param name="semiObjId">semiObjId.</param>
        /// <param name="conditions">Required/Used for EDA Trace Request.</param>
        public TriggerEventRequest(string triggerRequestType = default(string), string eventId = default(string), string semiObjType = default(string), string semiObjId = default(string), List<TraceConditions> conditions = default(List<TraceConditions>))
        {
            this.TriggerRequestType = triggerRequestType;
            this.EventId = eventId;
            this.SemiObjType = semiObjType;
            this.SemiObjId = semiObjId;
            this.Conditions = conditions;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; private set; }

        /// <summary>
        /// TriggerEventRequest
        /// </summary>
        /// <value>TriggerEventRequest</value>
        [DataMember(Name = "triggerRequestType", EmitDefaultValue = false)]
        public string TriggerRequestType { get; set; }

        /// <summary>
        /// Required/Used for EDA Trace Request
        /// </summary>
        /// <value>Required/Used for EDA Trace Request</value>
        [DataMember(Name = "eventId", EmitDefaultValue = false)]
        public string EventId { get; set; }

        /// <summary>
        /// Gets or Sets SemiObjType
        /// </summary>
        [DataMember(Name = "semiObjType", EmitDefaultValue = false)]
        public string SemiObjType { get; set; }

        /// <summary>
        /// Gets or Sets SemiObjId
        /// </summary>
        [DataMember(Name = "semiObjId", EmitDefaultValue = false)]
        public string SemiObjId { get; set; }

        /// <summary>
        /// Required/Used for EDA Trace Request
        /// </summary>
        /// <value>Required/Used for EDA Trace Request</value>
        [DataMember(Name = "conditions", EmitDefaultValue = false)]
        public List<TraceConditions> Conditions { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        /// <value>String representation in UI</value>
        public string DisplayName
        {
            get
            {
                //var parameterCondition = $"{Conditions[0].ParameterId.Substring(Conditions[0].ParameterId.LastIndexOf(':')+1)} {Conditions[0]._Operator} {Conditions[0].ParameterValue}";
                //var eventString = EventId.Substring(EventId.LastIndexOf(':')+1);
                //return $"{eventString} : {parameterCondition}";

                return EventId.Substring(EventId.LastIndexOf(':') + 1);
            }
        }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TriggerEventRequest {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  TriggerRequestType: ").Append(TriggerRequestType).Append("\n");
            sb.Append("  EventId: ").Append(EventId).Append("\n");
            sb.Append("  SemiObjType: ").Append(SemiObjType).Append("\n");
            sb.Append("  SemiObjId: ").Append(SemiObjId).Append("\n");
            sb.Append("  Conditions: ").Append(Conditions).Append("\n");
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
            return this.Equals(input as TriggerEventRequest);
        }

        /// <summary>
        /// Returns true if TriggerEventRequest instances are equal
        /// </summary>
        /// <param name="input">Instance of TriggerEventRequest to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TriggerEventRequest input)
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
                    this.TriggerRequestType == input.TriggerRequestType ||
                    (this.TriggerRequestType != null &&
                    this.TriggerRequestType.Equals(input.TriggerRequestType))
                ) &&
                (
                    this.EventId == input.EventId ||
                    (this.EventId != null &&
                    this.EventId.Equals(input.EventId))
                ) &&
                (
                    this.SemiObjType == input.SemiObjType ||
                    (this.SemiObjType != null &&
                    this.SemiObjType.Equals(input.SemiObjType))
                ) &&
                (
                    this.SemiObjId == input.SemiObjId ||
                    (this.SemiObjId != null &&
                    this.SemiObjId.Equals(input.SemiObjId))
                ) &&
                (
                    this.Conditions == input.Conditions ||
                    this.Conditions != null &&
                    input.Conditions != null &&
                    this.Conditions.SequenceEqual(input.Conditions)
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
                if( this.TriggerRequestType != null )
                    hashCode = hashCode * 59 + this.TriggerRequestType.GetHashCode();
                if( this.EventId != null )
                    hashCode = hashCode * 59 + this.EventId.GetHashCode();
                if( this.SemiObjType != null )
                    hashCode = hashCode * 59 + this.SemiObjType.GetHashCode();
                if( this.SemiObjId != null )
                    hashCode = hashCode * 59 + this.SemiObjId.GetHashCode();
                if( this.Conditions != null )
                    hashCode = hashCode * 59 + this.Conditions.GetHashCode();
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
