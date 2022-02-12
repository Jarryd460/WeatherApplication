using System.ComponentModel.DataAnnotations;

namespace Weather.Api.Models
{
    public class WeatherForecast
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        /// <example>1</example>
        [Key, Required]
        public long Id { get; set; }

        /// <summary>
        /// Date of the weather forecast
        /// </summary>
        /// <example>2022-01-28T18:29:19.224Z</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature of weather in celcius
        /// </summary>
        /// <example>55</example>
        [Range(-273, 100, ErrorMessage = "Temperature in celcius has to be between -273 and 100 degrees celcius")]
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temperature of weather in fahrenheit
        /// </summary>
        /// <example>120</example>
        [Range(-459, 200, ErrorMessage = "Temperature in fahrenheit has to be between -459 and 200 degrees fahrenheit")]
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Summarization of weather forecast
        /// </summary>
        /// <example>Partly cloudy with a chance of rain</example>
        [MaxLength(150)]
        public string? Summary { get; set; }
    }
}