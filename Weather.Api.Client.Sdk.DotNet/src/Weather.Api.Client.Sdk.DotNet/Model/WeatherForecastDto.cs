/*
 * API
 *
 * An Api to perform weather forecasts
 *
 * The version of the OpenAPI document: 1
 * Contact: jon.doe@gmail.com
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Weather.Api.Client.Sdk.DotNet.Client.OpenAPIDateConverter;

namespace Weather.Api.Client.Sdk.DotNet.Model
{
    /// <summary>
    /// WeatherForecastDto
    /// </summary>
    [DataContract(Name = "WeatherForecastDto")]
    public partial class WeatherForecastDto : IEquatable<WeatherForecastDto>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastDto" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected WeatherForecastDto() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastDto" /> class.
        /// </summary>
        /// <param name="id">Unique identifier (required).</param>
        /// <param name="date">Date of the weather forecast (required).</param>
        /// <param name="temperatureC">Temperature of weather in celcius.</param>
        /// <param name="summary">Summarization of weather forecast.</param>
        public WeatherForecastDto(long id = default(long), DateTime date = default(DateTime), int temperatureC = default(int), string summary = default(string))
        {
            this.Id = id;
            this.Date = date;
            this.TemperatureC = temperatureC;
            this.Summary = summary;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <value>Unique identifier</value>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = false)]
        public long Id { get; set; }

        /// <summary>
        /// Date of the weather forecast
        /// </summary>
        /// <value>Date of the weather forecast</value>
        [DataMember(Name = "date", IsRequired = true, EmitDefaultValue = false)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature of weather in celcius
        /// </summary>
        /// <value>Temperature of weather in celcius</value>
        [DataMember(Name = "temperatureC", EmitDefaultValue = false)]
        public int TemperatureC { get; set; }

        /// <summary>
        /// Summarization of weather forecast
        /// </summary>
        /// <value>Summarization of weather forecast</value>
        [DataMember(Name = "summary", EmitDefaultValue = true)]
        public string Summary { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class WeatherForecastDto {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Date: ").Append(Date).Append("\n");
            sb.Append("  TemperatureC: ").Append(TemperatureC).Append("\n");
            sb.Append("  Summary: ").Append(Summary).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as WeatherForecastDto);
        }

        /// <summary>
        /// Returns true if WeatherForecastDto instances are equal
        /// </summary>
        /// <param name="input">Instance of WeatherForecastDto to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WeatherForecastDto input)
        {
            if (input == null)
            {
                return false;
            }
            return 
                (
                    this.Id == input.Id ||
                    this.Id.Equals(input.Id)
                ) && 
                (
                    this.Date == input.Date ||
                    (this.Date != null &&
                    this.Date.Equals(input.Date))
                ) && 
                (
                    this.TemperatureC == input.TemperatureC ||
                    this.TemperatureC.Equals(input.TemperatureC)
                ) && 
                (
                    this.Summary == input.Summary ||
                    (this.Summary != null &&
                    this.Summary.Equals(input.Summary))
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
                hashCode = (hashCode * 59) + this.Id.GetHashCode();
                if (this.Date != null)
                {
                    hashCode = (hashCode * 59) + this.Date.GetHashCode();
                }
                hashCode = (hashCode * 59) + this.TemperatureC.GetHashCode();
                if (this.Summary != null)
                {
                    hashCode = (hashCode * 59) + this.Summary.GetHashCode();
                }
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            // TemperatureC (int) maximum
            if (this.TemperatureC > (int)100)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TemperatureC, must be a value less than or equal to 100.", new [] { "TemperatureC" });
            }

            // TemperatureC (int) minimum
            if (this.TemperatureC < (int)-273)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TemperatureC, must be a value greater than or equal to -273.", new [] { "TemperatureC" });
            }

            // Summary (string) maxLength
            if (this.Summary != null && this.Summary.Length > 150)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Summary, length must be less than 150.", new [] { "Summary" });
            }

            yield break;
        }
    }

}
