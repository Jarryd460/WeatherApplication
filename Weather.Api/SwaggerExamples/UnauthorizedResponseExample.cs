﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Weather.Api.SwaggerExamples
{
    public class UnauthorizedResponseExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            var problemDetails = new ProblemDetails()
            {
                Type = "https://httpstatuses.com/401",
                Title = nameof(HttpStatusCode.Unauthorized),
                Status = (int)HttpStatusCode.Unauthorized
            };

            problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

            return problemDetails;
        }
    }
}
