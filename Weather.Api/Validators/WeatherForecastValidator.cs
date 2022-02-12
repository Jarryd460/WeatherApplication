using FluentValidation;
using Weather.Api.DTOs;

namespace Weather.Api.Validators
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecastDto>
    {
        public WeatherForecastValidator()
        {
            RuleFor(weatherForecast => weatherForecast.Date).NotNull();
            RuleFor(weatherForecast => weatherForecast.Date).GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
