using System.ComponentModel.DataAnnotations;

namespace Weather.Api.DTOs
{
    public class WeatherForecastDto
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
        public DateTime Date { get; init; }

        /// <summary>
        /// Temperature of weather in celcius
        /// </summary>
        /// <example>55</example>
        [Range(-273, 100, ErrorMessage = "Temperature in celcius has to be between -273 and 100 degrees celcius")]
        public int TemperatureC { get; init; }

        /// <summary>
        /// Summarization of weather forecast
        /// </summary>
        /// <example>Partly cloudy with a chance of rain</example>
        [MaxLength(150)]
        public string? Summary { get; init; }
    }
}
