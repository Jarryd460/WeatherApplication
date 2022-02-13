# Weather Application with Open Api/Swagger and SDK Generation

### Description

A Web Api project demonstrating the generation of Open Api/Swagger documentation and the use of this documentation to generate Swagger UI and SDK's, both client (node typescript) and server (C# dotnet)

### Dependencies:

* Microsoft.EntityFrameworkCore.InMemory
* Microsoft.AspNetCore.Identity.EntityFrameworkCore
* Microsoft.AspNetCore.Authentication.JwtBearer
* Swashbuckle.AspNetCore
* Swashbuckle.AspNetCore.Cli
* @openapitools/openapi-generator-cli
* FluentValidation.AspNetCore
* MicroElements.Swashbuckle.FluentValidation
* Hellang.Middleware.ProblemDetails
* Swashbuckle.AspNetCore.Annotations 
* Swashbuckle.AspNetCore.Filters

### Generate Open Api/Swagger Documentation

* Install swashbuckle cli tool by running "dotnet tool install Swashbuckle.AspNetCore.Cli --global".
* Once installed, run "dotnet swagger tofile --output swagger.json Api.dll v1".
* The above has been built into the pre build for the Api.
* Reference: https://medium.com/@woeterman_94/how-to-generate-a-swagger-json-file-on-build-in-net-core-fa74eec3df1

### Generate SDK

* Install node (https://nodejs.org/) and run "npm install @openapitools/openapi-generator-cli -g".
* Run "openapi-generator-cli generate -i swagger.json -g csharp-netcore -o Weather.Api.Client.Sdk --additional-properties packageName=Weather.Api.Client.Sdk" in the location where you would like the sdk to be generated.
* The above has been built into the pre build for the Api.
* Reference: 
	* https://openapi-generator.tech
	* https://openapi-generator.tech/docs/installation/
	* https://openapi-generator.tech/docs/generators/
	* https://medium.com/@no0law1/generate-client-sdk-for-net-core-using-open-api-no0law1-4767fa86a17f
	* https://github.com/no0law1/open-api-client-sdk-generator-tutorial


### Additional Documentation

* Globally set return types for all endpoints: https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-6.0
* Swashbuckle: 
	* https://github.com/domaindrivendev/Swashbuckle.AspNetCore
	* https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/master/README.md#assign-explicit-operationids
	* https://www.dotnetnakama.com/blog/enriched-web-api-documentation-using-swagger-openapi-in-asp-dotnet-core/
	* https://swagger.io/docs/specification/data-models/inheritance-and-polymorphism/
	* https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters
* MSBuild macros: https://docs.microsoft.com/en-us/cpp/build/reference/common-macros-for-build-commands-and-properties?view=msvc-170
* https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio
* Swashbuckle: https://github.com/domaindrivendev/Swashbuckle.AspNetCore#add-security-definitions-and-requirements
* Add authentication with Jwt:
	* https://medium.com/swlh/securing-your-net-core-3-api-using-identity-93d6426d6311
	* https://www.c-sharpcorner.com/article/jwt-authentication-and-authorization-in-net-6-0-with-identity-framework/
* Http error response standard (IETF RFC 7807): https://datatracker.ietf.org/doc/html/rfc7807
* Hellang.Middleware.ProblemDetails
	* https://code-maze.com/using-the-problemdetails-class-in-asp-net-core-web-api/amp/
	* https://github.com/khellang/Middleware
* Tools that take advantage of OpenApi documents: https://openapi.tools/#mock
* OpenApi Generator examples: https://github.com/OpenAPITools/openapi-generator/tree/master/samples/client/petstore/typescript-node/npm
