using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Weather.Api.DTOs;
using Weather.Api.Models;
using Weather.Api.SwaggerExamples;

namespace Weather.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherContext weatherContext)
        {
            _logger = logger;
            _context = weatherContext;
        }

        /// <summary>
        /// Gets all weather forecasts
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns all weather forecasts</response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpGet(Name = nameof(GetWeatherForecasts))]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> GetWeatherForecasts(CancellationToken cancellationToken)
        {
            return await _context.Forecasts
                .Select(forecast => WeatherForecastToDto(forecast))
                .ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the weather forecast with the specified id
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/weatherforecast/1
        ///
        /// </remarks>
        /// <response code="200">Returns the weather forecast with the specified Id</response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="403">When user has not been authorized to access the resource</response>
        /// <response code="404">If the weather forecast with the specified id does not exist</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [Authorize(Roles = UserRole.Admin)]
        [HttpGet("{id}", Name = nameof(GetWeatherForecast))]
        [ProducesResponseType(typeof(WeatherForecastDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [SwaggerResponseExample((int)HttpStatusCode.Forbidden, typeof(ForbiddenResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<ActionResult<WeatherForecastDto>> GetWeatherForecast([FromRoute, Required] long id, CancellationToken cancellationToken)
        {
            var forecast = await _context.Forecasts.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

            if (forecast == null)
            {
                return NotFound();
            }

            return WeatherForecastToDto(forecast);
        }

        /// <summary>
        /// A hidden endpoint that should not be generated as part of Open Api document and SDK
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/HiddenEndpoint
        ///
        /// </remarks>
        /// <response code="204"></response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="403">When user has not been authorized to access the resource</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        /// Sets the endpoint to not be generated as part of the Open Api and subsequently the SDK
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("HiddenEndpoint", Name = nameof(HiddenEndpoint))]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [SwaggerResponseExample((int)HttpStatusCode.Forbidden, typeof(ForbiddenResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<ActionResult> HiddenEndpoint(CancellationToken cancellationToken)
        {
            return NoContent();
        }

        /// <summary>
        /// Creates a weather forecast
        /// </summary>
        /// <param name="weatherForecast"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/weatherforecast
        ///     {
        ///        "id": 1,
        ///        "date": "2022-01-28T18:29:19.224Z",
        ///        "temperaturec": 47,
        ///        "summary": "Cloudy but very hot and humid"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created weather forecast</response>
        /// <response code="400">If the item is null or the values of the fields are incorrect</response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="403">When user has not been authorized to access the resource</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [Authorize("JaneDoe")]
        [HttpPost(Name = nameof(PostWeatherForecast))]
        [ProducesResponseType(typeof(WeatherForecastDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(BadRequestResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [SwaggerResponseExample((int)HttpStatusCode.Forbidden, typeof(ForbiddenResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<ActionResult<WeatherForecastDto>> PostWeatherForecast([FromBody, Required] WeatherForecastDto weatherForecast, CancellationToken cancellationToken)
        {
            var forecast = new WeatherForecast()
            {
                Id = weatherForecast.Id,
                Date = weatherForecast.Date,
                TemperatureC = weatherForecast.TemperatureC,
                Summary = weatherForecast.Summary
            };

            _context.Forecasts.Add(forecast);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetWeatherForecast), new { id = weatherForecast.Id }, weatherForecast);
        }

        /// <summary>
        /// Updates a weather forecast
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast</param>
        /// <param name="weatherForecast"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/weatherforecast/1
        ///     {
        ///        "id": 1,
        ///        "date": "2022-01-28T18:29:19.224Z",
        ///        "temperaturec": 47,
        ///        "summary": "Cloudy but very hot and humid"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If the weather forecast was updated</response>
        /// <response code="400">If the id does not match the id of the weather forecast or the <paramref name="weatherForecast"/> object values does not meet the criteria</response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="404">If the weather forecast with the specified id does not exist</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpPut("{id}", Name = nameof(PutWeatherForecast))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(BadRequestResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> PutWeatherForecast([FromRoute, Required] long id, [FromBody, Required] WeatherForecastDto weatherForecast, CancellationToken cancellationToken)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            var forecast = await _context.Forecasts.FindAsync(id, cancellationToken).ConfigureAwait(false);

            if (forecast == null)
            {
                return NotFound();
            }

            forecast.Date = weatherForecast.Date;
            forecast.TemperatureC = weatherForecast.TemperatureC;
            forecast.Summary = weatherForecast.Summary;

            try
            {
                await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await WeatherForecastExists(id, cancellationToken))
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a weather forecast
        /// </summary>
        /// <param name="id">The unique identifier of the weather forecast</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/weatherforecast/1
        ///
        /// </remarks>
        /// <response code="204">If the weather forecast was deleted</response>
        /// <response code="401">When user has not been authenticated or is unauthorized based on their role</response>
        /// <response code="404">If the weather forecast with the specified id does not exist</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpDelete("{id}", Name = nameof(DeleteWeatherForecast))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(InternalServerErrorResponseExample))]
        public async Task<IActionResult> DeleteWeatherForecast([FromRoute, Required] long id, CancellationToken cancellationToken)
        {
            var todoItem = await _context.Forecasts.FindAsync(new object[] { id }, cancellationToken).ConfigureAwait(false);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Forecasts.Remove(todoItem);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return NoContent();
        }

        private async Task<bool> WeatherForecastExists(long id, CancellationToken cancellationToken)
        {
            return await _context.Forecasts.AnyAsync(forecast => forecast.Id == id, cancellationToken).ConfigureAwait(false);
        }

        private static WeatherForecastDto WeatherForecastToDto(WeatherForecast weatherForecast) =>
            new WeatherForecastDto
            {
                Id = weatherForecast.Id,
                Date = weatherForecast.Date,
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
            };
    }
}