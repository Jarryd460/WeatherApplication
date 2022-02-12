using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Weather.Console.Dotnet
{
    public static class WeatherForecastApiExtensions
    {
        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;WeatherForecast&gt;</returns>
        public async static Task<WeatherForecastDto> GetFirstWeatherForecastAsync(this WeatherForecastApi weatherForecast)
        {            
            ApiResponse<List<WeatherForecastDto>> localVarResponse = await weatherForecast.GetWeatherForecastsWithHttpInfoAsync();
            return localVarResponse.Data.First();
        }
    }
}
