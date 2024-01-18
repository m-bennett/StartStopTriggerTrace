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
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StartStopTriggerTrace.Models
{
    /// <summary>
    /// Wrapper to hold pagination and sorting information.
    /// </summary>
    [DataContract]
        public partial class Page<T> :  IEquatable<Page<T>>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page" /> class.
        /// </summary>
        /// <param name="content">all the elements. (required).</param>
        /// <param name="pageable">pageable (required).</param>
        /// <param name="last">Whether this is the last page. (required).</param>
        /// <param name="totalPages">How many pages are there in total for this dataset. (required).</param>
        /// <param name="totalElements">How many elements are there in total for this dataset. (required).</param>
        /// <param name="size">Page size. (required).</param>
        /// <param name="number">Current page number. Note that page numbers start with 0. (required).</param>
        /// <param name="sort">sort (required).</param>
        /// <param name="numberOfElements">Number of elements on this page. (required).</param>
        /// <param name="first">Whether this is the first page. (required).</param>
        /// <param name="empty">Whether this page is empty. (required).</param>
        public Page(List<T> content = default(List<T>), PagePageable pageable = default(PagePageable), bool? last = default(bool?), int? totalPages = default(int?), int? totalElements = default(int?), int? size = default(int?), int? number = default(int?), PagePageableSort sort = default(PagePageableSort), int? numberOfElements = default(int?), bool? first = default(bool?), bool? empty = default(bool?))
        {
            // to ensure "content" is required (not null)
            if (content == null)
            {
                throw new InvalidDataException("content is a required property for Page and cannot be null");
            }
            else
            {
                this.Content = content;
            }
            // to ensure "pageable" is required (not null)
            if (pageable == null)
            {
                throw new InvalidDataException("pageable is a required property for Page and cannot be null");
            }
            else
            {
                this.Pageable = pageable;
            }
            // to ensure "last" is required (not null)
            if (last == null)
            {
                throw new InvalidDataException("last is a required property for Page and cannot be null");
            }
            else
            {
                this.Last = last;
            }
            // to ensure "totalPages" is required (not null)
            if (totalPages == null)
            {
                throw new InvalidDataException("totalPages is a required property for Page and cannot be null");
            }
            else
            {
                this.TotalPages = totalPages;
            }
            // to ensure "totalElements" is required (not null)
            if (totalElements == null)
            {
                throw new InvalidDataException("totalElements is a required property for Page and cannot be null");
            }
            else
            {
                this.TotalElements = totalElements;
            }
            // to ensure "size" is required (not null)
            if (size == null)
            {
                throw new InvalidDataException("size is a required property for Page and cannot be null");
            }
            else
            {
                this.Size = size;
            }
            // to ensure "number" is required (not null)
            if (number == null)
            {
                throw new InvalidDataException("number is a required property for Page and cannot be null");
            }
            else
            {
                this.Number = number;
            }
            // to ensure "sort" is required (not null)
            if (sort == null)
            {
                throw new InvalidDataException("sort is a required property for Page and cannot be null");
            }
            else
            {
                this.Sort = sort;
            }
            // to ensure "numberOfElements" is required (not null)
            if (numberOfElements == null)
            {
                throw new InvalidDataException("numberOfElements is a required property for Page and cannot be null");
            }
            else
            {
                this.NumberOfElements = numberOfElements;
            }
            // to ensure "first" is required (not null)
            if (first == null)
            {
                throw new InvalidDataException("first is a required property for Page and cannot be null");
            }
            else
            {
                this.First = first;
            }
            // to ensure "empty" is required (not null)
            if (empty == null)
            {
                throw new InvalidDataException("empty is a required property for Page and cannot be null");
            }
            else
            {
                this.Empty = empty;
            }
        }
        
        /// <summary>
        /// all the elements.
        /// </summary>
        /// <value>all the elements.</value>
        [DataMember(Name="content", EmitDefaultValue=false)]
        public List<T> Content { get; set; }

        /// <summary>
        /// Gets or Sets Pageable
        /// </summary>
        [DataMember(Name="pageable", EmitDefaultValue=false)]
        public PagePageable Pageable { get; set; }

        /// <summary>
        /// Whether this is the last page.
        /// </summary>
        /// <value>Whether this is the last page.</value>
        [DataMember(Name="last", EmitDefaultValue=false)]
        public bool? Last { get; set; }

        /// <summary>
        /// How many pages are there in total for this dataset.
        /// </summary>
        /// <value>How many pages are there in total for this dataset.</value>
        [DataMember(Name="totalPages", EmitDefaultValue=false)]
        public int? TotalPages { get; set; }

        /// <summary>
        /// How many elements are there in total for this dataset.
        /// </summary>
        /// <value>How many elements are there in total for this dataset.</value>
        [DataMember(Name="totalElements", EmitDefaultValue=false)]
        public int? TotalElements { get; set; }

        /// <summary>
        /// Page size.
        /// </summary>
        /// <value>Page size.</value>
        [DataMember(Name="size", EmitDefaultValue=false)]
        public int? Size { get; set; }

        /// <summary>
        /// Current page number. Note that page numbers start with 0.
        /// </summary>
        /// <value>Current page number. Note that page numbers start with 0.</value>
        [DataMember(Name="number", EmitDefaultValue=false)]
        public int? Number { get; set; }

        /// <summary>
        /// Gets or Sets Sort
        /// </summary>
        [DataMember(Name="sort", EmitDefaultValue=false)]
        public PagePageableSort Sort { get; set; }

        /// <summary>
        /// Number of elements on this page.
        /// </summary>
        /// <value>Number of elements on this page.</value>
        [DataMember(Name="numberOfElements", EmitDefaultValue=false)]
        public int? NumberOfElements { get; set; }

        /// <summary>
        /// Whether this is the first page.
        /// </summary>
        /// <value>Whether this is the first page.</value>
        [DataMember(Name="first", EmitDefaultValue=false)]
        public bool? First { get; set; }

        /// <summary>
        /// Whether this page is empty.
        /// </summary>
        /// <value>Whether this page is empty.</value>
        [DataMember(Name="empty", EmitDefaultValue=false)]
        public bool? Empty { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Page {\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  Pageable: ").Append(Pageable).Append("\n");
            sb.Append("  Last: ").Append(Last).Append("\n");
            sb.Append("  TotalPages: ").Append(TotalPages).Append("\n");
            sb.Append("  TotalElements: ").Append(TotalElements).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Number: ").Append(Number).Append("\n");
            sb.Append("  Sort: ").Append(Sort).Append("\n");
            sb.Append("  NumberOfElements: ").Append(NumberOfElements).Append("\n");
            sb.Append("  First: ").Append(First).Append("\n");
            sb.Append("  Empty: ").Append(Empty).Append("\n");
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
            return this.Equals(input as Page<T>);
        }

        /// <summary>
        /// Returns true if Page instances are equal
        /// </summary>
        /// <param name="input">Instance of Page to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Page<T> input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Content == input.Content ||
                    this.Content != null &&
                    input.Content != null &&
                    this.Content.SequenceEqual(input.Content)
                ) && 
                (
                    this.Pageable == input.Pageable ||
                    (this.Pageable != null &&
                    this.Pageable.Equals(input.Pageable))
                ) && 
                (
                    this.Last == input.Last ||
                    (this.Last != null &&
                    this.Last.Equals(input.Last))
                ) && 
                (
                    this.TotalPages == input.TotalPages ||
                    (this.TotalPages != null &&
                    this.TotalPages.Equals(input.TotalPages))
                ) && 
                (
                    this.TotalElements == input.TotalElements ||
                    (this.TotalElements != null &&
                    this.TotalElements.Equals(input.TotalElements))
                ) && 
                (
                    this.Size == input.Size ||
                    (this.Size != null &&
                    this.Size.Equals(input.Size))
                ) && 
                (
                    this.Number == input.Number ||
                    (this.Number != null &&
                    this.Number.Equals(input.Number))
                ) && 
                (
                    this.Sort == input.Sort ||
                    (this.Sort != null &&
                    this.Sort.Equals(input.Sort))
                ) && 
                (
                    this.NumberOfElements == input.NumberOfElements ||
                    (this.NumberOfElements != null &&
                    this.NumberOfElements.Equals(input.NumberOfElements))
                ) && 
                (
                    this.First == input.First ||
                    (this.First != null &&
                    this.First.Equals(input.First))
                ) && 
                (
                    this.Empty == input.Empty ||
                    (this.Empty != null &&
                    this.Empty.Equals(input.Empty))
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
                if (this.Content != null)
                    hashCode = hashCode * 59 + this.Content.GetHashCode();
                if (this.Pageable != null)
                    hashCode = hashCode * 59 + this.Pageable.GetHashCode();
                if (this.Last != null)
                    hashCode = hashCode * 59 + this.Last.GetHashCode();
                if (this.TotalPages != null)
                    hashCode = hashCode * 59 + this.TotalPages.GetHashCode();
                if (this.TotalElements != null)
                    hashCode = hashCode * 59 + this.TotalElements.GetHashCode();
                if (this.Size != null)
                    hashCode = hashCode * 59 + this.Size.GetHashCode();
                if (this.Number != null)
                    hashCode = hashCode * 59 + this.Number.GetHashCode();
                if (this.Sort != null)
                    hashCode = hashCode * 59 + this.Sort.GetHashCode();
                if (this.NumberOfElements != null)
                    hashCode = hashCode * 59 + this.NumberOfElements.GetHashCode();
                if (this.First != null)
                    hashCode = hashCode * 59 + this.First.GetHashCode();
                if (this.Empty != null)
                    hashCode = hashCode * 59 + this.Empty.GetHashCode();
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