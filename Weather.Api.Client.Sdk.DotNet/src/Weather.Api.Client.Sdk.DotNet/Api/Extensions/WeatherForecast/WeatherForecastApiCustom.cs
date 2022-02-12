using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Weather.Api.Client.Sdk.DotNet.Api
{
    public partial class WeatherForecastApi
    {
        /// <summary>
        ///  
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>List&lt;WeatherForecast&gt;</returns>
        public async Task<WeatherForecastDto> GetLastWeatherForecastAsync()
        {
            ApiResponse<List<WeatherForecastDto>> localVarResponse = await GetWeatherForecastsWithHttpInfoAsync();
            return localVarResponse.Data.Last();
        }
    }
}
