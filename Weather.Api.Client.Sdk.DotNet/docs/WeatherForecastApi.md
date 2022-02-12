# Weather.Api.Client.Sdk.DotNet.Api.WeatherForecastApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**DeleteWeatherForecast**](WeatherForecastApi.md#deleteweatherforecast) | **DELETE** /api/WeatherForecast/{id} | Deletes a weather forecast
[**GetWeatherForecast**](WeatherForecastApi.md#getweatherforecast) | **GET** /api/WeatherForecast/{id} | Gets the weather forecast with the specified id (Auth roles: Admin)
[**GetWeatherForecasts**](WeatherForecastApi.md#getweatherforecasts) | **GET** /api/WeatherForecast | Gets all weather forecasts
[**PostWeatherForecast**](WeatherForecastApi.md#postweatherforecast) | **POST** /api/WeatherForecast | Creates a weather forecast (Auth policies: JaneDoe)
[**PutWeatherForecast**](WeatherForecastApi.md#putweatherforecast) | **PUT** /api/WeatherForecast/{id} | Updates a weather forecast


<a name="deleteweatherforecast"></a>
# **DeleteWeatherForecast**
> void DeleteWeatherForecast (long id)

Deletes a weather forecast

Sample request:                    DELETE /api/weatherforecast/1

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class DeleteWeatherForecastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WeatherForecastApi(config);
            var id = 789;  // long | The unique identifier of the weather forecast

            try
            {
                // Deletes a weather forecast
                apiInstance.DeleteWeatherForecast(id);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.DeleteWeatherForecast: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **long**| The unique identifier of the weather forecast | 

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **401** | When user has not been authenticated or is unauthorized based on their role |  -  |
| **404** | If the weather forecast with the specified id does not exist |  -  |
| **415** | When content type of request or response is not allowed |  -  |
| **500** | When something unexpected has happened |  -  |
| **204** | If the weather forecast was deleted |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getweatherforecast"></a>
# **GetWeatherForecast**
> WeatherForecastDto GetWeatherForecast (long id)

Gets the weather forecast with the specified id (Auth roles: Admin)

Sample request:                    GET /api/weatherforecast/1

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class GetWeatherForecastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WeatherForecastApi(config);
            var id = 789;  // long | The unique identifier of the weather forecast

            try
            {
                // Gets the weather forecast with the specified id (Auth roles: Admin)
                WeatherForecastDto result = apiInstance.GetWeatherForecast(id);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.GetWeatherForecast: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **long**| The unique identifier of the weather forecast | 

### Return type

[**WeatherForecastDto**](WeatherForecastDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns the weather forecast with the specified Id |  -  |
| **401** | When user has not been authenticated or is unauthorized based on their role |  -  |
| **403** | When user has not been authorized to access the resource |  -  |
| **404** | If the weather forecast with the specified id does not exist |  -  |
| **415** | When content type of request or response is not allowed |  -  |
| **500** | When something unexpected has happened |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getweatherforecasts"></a>
# **GetWeatherForecasts**
> List&lt;WeatherForecastDto&gt; GetWeatherForecasts ()

Gets all weather forecasts

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class GetWeatherForecastsExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WeatherForecastApi(config);

            try
            {
                // Gets all weather forecasts
                List<WeatherForecastDto> result = apiInstance.GetWeatherForecasts();
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.GetWeatherForecasts: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List&lt;WeatherForecastDto&gt;**](WeatherForecastDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns all weather forecasts |  -  |
| **401** | When user has not been authenticated or is unauthorized based on their role |  -  |
| **415** | When content type of request or response is not allowed |  -  |
| **500** | When something unexpected has happened |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="postweatherforecast"></a>
# **PostWeatherForecast**
> WeatherForecastDto PostWeatherForecast (WeatherForecastDto weatherForecastDto)

Creates a weather forecast (Auth policies: JaneDoe)

Sample request:                    POST /api/weatherforecast      {         \"id\": 1,         \"date\": \"2022-01-28T18:29:19.224Z\",         \"temperaturec\": 47,         \"summary\": \"Cloudy but very hot and humid\"      }

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class PostWeatherForecastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WeatherForecastApi(config);
            var weatherForecastDto = new WeatherForecastDto(); // WeatherForecastDto | 

            try
            {
                // Creates a weather forecast (Auth policies: JaneDoe)
                WeatherForecastDto result = apiInstance.PostWeatherForecast(weatherForecastDto);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.PostWeatherForecast: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **weatherForecastDto** | [**WeatherForecastDto**](WeatherForecastDto.md)|  | 

### Return type

[**WeatherForecastDto**](WeatherForecastDto.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **201** | Returns the newly created weather forecast |  -  |
| **400** | If the item is null or the values of the fields are incorrect |  -  |
| **401** | When user has not been authenticated or is unauthorized based on their role |  -  |
| **403** | When user has not been authorized to access the resource |  -  |
| **415** | When content type of request or response is not allowed |  -  |
| **500** | When something unexpected has happened |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="putweatherforecast"></a>
# **PutWeatherForecast**
> void PutWeatherForecast (long id, WeatherForecastDto weatherForecastDto)

Updates a weather forecast

Sample request:                    PUT /api/weatherforecast/1      {         \"id\": 1,         \"date\": \"2022-01-28T18:29:19.224Z\",         \"temperaturec\": 47,         \"summary\": \"Cloudy but very hot and humid\"      }

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class PutWeatherForecastExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            // Configure API key authorization: Bearer
            config.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // config.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WeatherForecastApi(config);
            var id = 789;  // long | The unique identifier of the weather forecast
            var weatherForecastDto = new WeatherForecastDto(); // WeatherForecastDto | 

            try
            {
                // Updates a weather forecast
                apiInstance.PutWeatherForecast(id, weatherForecastDto);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.PutWeatherForecast: " + e.Message );
                Debug.Print("Status Code: "+ e.ErrorCode);
                Debug.Print(e.StackTrace);
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | **long**| The unique identifier of the weather forecast | 
 **weatherForecastDto** | [**WeatherForecastDto**](WeatherForecastDto.md)|  | 

### Return type

void (empty response body)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **400** | If the id does not match the id of the weather forecast or the weatherForecast object values does not meet the criteria |  -  |
| **401** | When user has not been authenticated or is unauthorized based on their role |  -  |
| **404** | If the weather forecast with the specified id does not exist |  -  |
| **415** | When content type of request or response is not allowed |  -  |
| **500** | When something unexpected has happened |  -  |
| **204** | If the weather forecast was updated |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

