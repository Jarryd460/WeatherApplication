# Weather.Api.Client.Sdk.DotNet.Api.AuthenticateApi

All URIs are relative to *http://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**Authenticate**](AuthenticateApi.md#authenticate) | **POST** /api/Authenticate | Authenticates a user


<a name="authenticate"></a>
# **Authenticate**
> Token Authenticate (LoginCredentials loginCredentials)

Authenticates a user

Sample request:                    POST /api/authenticate      {         \"username\": \"anon\",         \"password\": \"Anon123\"      }

### Example
```csharp
using System.Collections.Generic;
using System.Diagnostics;
using Weather.Api.Client.Sdk.DotNet.Api;
using Weather.Api.Client.Sdk.DotNet.Client;
using Weather.Api.Client.Sdk.DotNet.Model;

namespace Example
{
    public class AuthenticateExample
    {
        public static void Main()
        {
            Configuration config = new Configuration();
            config.BasePath = "http://localhost";
            var apiInstance = new AuthenticateApi(config);
            var loginCredentials = new LoginCredentials(); // LoginCredentials | 

            try
            {
                // Authenticates a user
                Token result = apiInstance.Authenticate(loginCredentials);
                Debug.WriteLine(result);
            }
            catch (ApiException  e)
            {
                Debug.Print("Exception when calling AuthenticateApi.Authenticate: " + e.Message );
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
 **loginCredentials** | [**LoginCredentials**](LoginCredentials.md)|  | 

### Return type

[**Token**](Token.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json, application/problem+json


### HTTP response details
| Status code | Description | Response headers |
|-------------|-------------|------------------|
| **200** | Returns a token |  -  |
| **400** | If the item is null |  -  |
| **401** | When user has provided an invalid username and password combination |  -  |

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

