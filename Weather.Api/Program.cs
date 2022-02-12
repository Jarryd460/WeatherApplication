using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using Weather.Api.Models;

// Adds the default attributes to endpoints based on their name and http verb attribute
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Weather.Api;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services
            .AddControllers(options =>
            {
                // Sets all endpoints to accept and produce only json (application/json)
                // The default formatter options.InputFormatters[0] (SystemTextJsonInputFormatter) still supports application/*.json and text/json by default
                options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
                options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));
                //options.Filters.Add(new ProducesDefaultResponseTypeAttribute());
                //options.Filters.Add(new ProducesResponseTypeAttribute());

                // Find all the input json formatters (formatters for handling request/body data)
                var jsonInputFormatters = options.InputFormatters.Where(formatter => formatter.GetType() == typeof(SystemTextJsonInputFormatter));
                // Get the supported media types of the json formatters
                var jsonInputSupportedMediaTypes = jsonInputFormatters.Select(formatter => ((SystemTextJsonInputFormatter)formatter).SupportedMediaTypes);

                foreach (MediaTypeCollection supportedMediaType in jsonInputSupportedMediaTypes)
                {
                    // Remove text/json and application/*+json as it is not allowed as part of the request body
                    // text/json is automatically not allowed without removing it but application/*+json is not and requires it's removal
                    // application/*+json comes up as a option when specifying the request body content type
                    supportedMediaType.Remove("text/json");
                    supportedMediaType.Remove("application/*+json");
                }

                // Remove the unnecessary formatters because we only need formatter for application/json
                options.OutputFormatters.RemoveType<StringOutputFormatter>();

                // Find all the output json formatters (formatters for handling response/body data)
                var jsonOutputFormatters = options.OutputFormatters.Where(formatter => formatter.GetType() == typeof(SystemTextJsonOutputFormatter));
                // Get the supported media types of the json formatters
                var jsonOutputSupportedMediaTypes = jsonOutputFormatters.Select(formatter => ((SystemTextJsonOutputFormatter)formatter).SupportedMediaTypes);

                foreach (MediaTypeCollection supportedMediaType in jsonOutputSupportedMediaTypes)
                {
                    // Remove text/json and application/*+json as it is not allowed as part of the response body
                    // Having ProducesAttribute added as a filter does not require the removal of text/json and application/*+json as media types but
                    // to be consistent, it's best to remove it
                    supportedMediaType.Remove("text/json");
                    supportedMediaType.Remove("application/*+json");
                    supportedMediaType.Add("application/problem+json");
                }

                // Returns a 406 Http status code if requested content type (accept header) is not application/json
                options.ReturnHttpNotAcceptable = true;

                // Adds Authorize attribute to all endpoints but can be overridden when attribute is specified on the endpoint directly
                var policy = new AuthorizationPolicyBuilder("Bearer").RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            // Adds fluent validation to the middleware and gets all the validators (classes that inherit from AbstractValidator) located in the same assembly as the "Program" class
            .AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblyContaining<Program>();
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services
            // The ApiController attribute on all controllers formats helper responses such as NotFound, Ok, BadRequest to a ProblemDetails object
            // which follows the standard for http responses (IETF RFC 7807) however unhandled exceptions are not automatically formatted.
            // Using the library Hellang.Middleware.ProblemDetails formats unhandled exceptions as well
            .AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, exception) => context.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment();
            })
            .AddEndpointsApiExplorer()
            // Add an in-memory entity framework database
            .AddDbContext<WeatherContext>(options =>
            {
                options.UseInMemoryDatabase("Forecast");
            });

        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Signin settings.
            options.SignIn.RequireConfirmedAccount = true;

            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        }).AddEntityFrameworkStores<WeatherContext>();

        builder.Services
            .AddAuthorization(options =>
            {
                options.AddPolicy("JaneDoe", policy =>
                {
                    policy.RequireUserName("JaneDoe");
                });
            })
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidIssuer = jwtSettings.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services
            .AddSwaggerGen(options =>
            {
                // Add general detail about the web api
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "1",
                    Description = "An Api to perform weather forecasts",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Jon Doe",
                        Email = "jon.doe@gmail.com",
                        Url = new Uri("https://twitter.com/jdoe")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Employee API LINCX",
                        Url = new Uri("https://example.com/license")
                    }
                });

                options.ExampleFilters();

                // Add / describe the authentication used by the web api as swashbuckle is not able to automatically pick it up
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    },
                    // TODO: what does this do
                    UnresolvedReference = true
                };

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);

                // Adds authentication documentation to all endpoints
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                //{
                //    {
                //        securitySchema,
                //        new[] {
                //            JwtBearerDefaults.AuthenticationScheme
                //        }
                //    }
                //});

                // Adds authentication for all endpoints not marked as anonymous with a lock image
                options.OperationFilter<Weather.Api.Filters.SecurityRequirementsOperationFilter>();

                // Instead of setting the name property on the route attribute for every method
                // We can do this globally
                options.CustomOperationIds(description =>
                {
                    return description.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

                // By default swagger flattens inheritance hierarchies, which means base class properties are duplicated on subtypes
                // In order to maintain the heirarchy, we can call UseAllForInheritance() or UseOneOfForPolymorphism
                options.UseAllOfForInheritance();
                //options.UseOneOfForPolymorphism();

                // Generate and use xml comments to enhance the swagger/Open API documentation
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                // Adds text to each endpoint to show when its authorized and the policy/claims on it eg. (Auth: policies: JaneDoe) or (Auth: roles: Admin)
                // Must be added anywhere after loading xml comments
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            })
            // Besides the attributes placed on properties to add validation criteria, it reads
            // and adds fluent validation rules to swagger documentation
            .AddFluentValidationRulesToSwagger()
            .AddSwaggerExamplesFromAssemblyOf<Program>();

        var app = builder.Build();

        app.UseProblemDetails();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // Displays the operation id/route name set on the controller endpoint ([Route(Name = "endpoint name")]) which is also the name openapi generator cli
                // will give the method when generating the SDK
                options.DisplayOperationId();
                // As the method suggests, displays the duration of the request
                options.DisplayRequestDuration();
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Ensure that the database is created at startup otherwise if endpoints are called before it has been created,
        // no results will be returned
        var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

        using (var serviceScope = serviceScopeFactory.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<WeatherContext>();
            dbContext.Database.EnsureCreated();

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            WeatherContextConfiguration weatherContextConfiguration = new WeatherContextConfiguration(userManager, roleManager);
            await weatherContextConfiguration.SeedDataAsync();
        }

        app.Run();
    }
}