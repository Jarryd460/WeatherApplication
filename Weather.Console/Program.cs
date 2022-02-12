// See https://aka.ms/new-console-template for more information
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;
using Weather.Console.Dotnet;

// You can make use of the general client to make requests but you are required to know the inputs and outputs of the endpoint you are
// attempting to hit
ApiClient apiClient = new ApiClient("https://localhost:7036");

// Generated both synchronous and asynchronous methods based on the return type of endpoints
// Synchronous methods are generated as the default
// In addition Asynchronous methods are generated if Task is returned by endpoints
var result = await apiClient.GetAsync<List<WeatherForecastDto>>("weatherforecast", new RequestOptions());

Console.WriteLine($"Generic api client class used:");
Console.WriteLine(result.StatusCode);

foreach (var item in result.Data)
{
    Console.WriteLine($"Id: {item.Id}");
    Console.WriteLine($"Date: {item.Date}");
    Console.WriteLine($"Summary: {item.Summary}");
    Console.WriteLine($"temperature in celsius: {item.TemperatureC}");
    Console.WriteLine("");
}

Console.WriteLine("===============================================================");

// The alternative to using the general api client is to use the specific api/controller that has the endpoint you are trying to hit
Configuration configuration = new Configuration()
{
    BasePath = "https://localhost:7036"
};

WeatherForecastApi weatherForecastApi = new WeatherForecastApi(configuration);

try
{
    var forecast = await weatherForecastApi.GetWeatherForecastsAsync();

    Console.WriteLine($"WeatherForecastApi class used:");

    foreach (var item in forecast)
    {
        Console.WriteLine($"Id: {item.Id}");
        Console.WriteLine($"Date: {item.Date}");
        Console.WriteLine($"Summary: {item.Summary}");
        Console.WriteLine($"temperature in celsius: {item.TemperatureC}");
        Console.WriteLine("");
    }
}
catch (ApiException ex)
{
    Console.WriteLine(ex.Message);
}

// There is multiple ways to extend the weatherForecastApi to include additional functionality such as a custom endpoint
// One being add extension methods
var extensionMethodWeatherForecast = await weatherForecastApi.GetFirstWeatherForecastAsync();

Console.WriteLine($"Custom extension method used:");
Console.WriteLine($"Id: {extensionMethodWeatherForecast.Id}");
Console.WriteLine($"Date: {extensionMethodWeatherForecast.Date}");
Console.WriteLine($"Summary: {extensionMethodWeatherForecast.Summary}");
Console.WriteLine($"temperature in celsius: {extensionMethodWeatherForecast.TemperatureC}");

// Another way to add additonal functionality is to make a partial class of the same name
// It requires the class to be created in the same library and namespace
var partialClassWeatherForecast = await weatherForecastApi.GetLastWeatherForecastAsync();

Console.WriteLine($"Partial class with custom method used:");
Console.WriteLine($"Id: {partialClassWeatherForecast.Id}");
Console.WriteLine($"Date: {partialClassWeatherForecast.Date}");
Console.WriteLine($"Summary: {partialClassWeatherForecast.Summary}");
Console.WriteLine($"temperature in celsius: {partialClassWeatherForecast.TemperatureC}");

// Call the hidden endpoint
var hiddenEndpointResult = await apiClient.GetAsync<object>("weatherforecast/hiddenendpoint", new RequestOptions());

Console.WriteLine($"Hidden endpoint called:");
Console.WriteLine($"Status Code: {hiddenEndpointResult.StatusCode}");