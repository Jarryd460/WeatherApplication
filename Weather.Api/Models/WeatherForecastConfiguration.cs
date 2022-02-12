using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weather.Api.Models
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasData(
               new WeatherForecast()
               {
                   Id = 1,
                   Date = DateTime.Now,
                   TemperatureC = 20,
                   Summary = "Partly cloudy with a chance of rain"
               },
               new WeatherForecast()
               {
                   Id = 2,
                   Date = DateTime.Now.AddDays(1),
                   TemperatureC = 25,
                   Summary = "Clear skies"
               },
               new WeatherForecast()
               {
                   Id = 3,
                   Date = DateTime.Now.AddDays(2),
                   TemperatureC = 24,
                   Summary = "Morning rain"
               }
           );
        }
    }
}
