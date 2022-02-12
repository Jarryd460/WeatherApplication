using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Weather.Api.Models;
using Weather.Api.SwaggerExamples;

namespace Weather.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="loginCredentials"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/authenticate
        ///     {
        ///        "username": "anon",
        ///        "password": "Anon123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns a token</response>
        /// <response code="400">If the item is null</response>
        /// <response code="401">When user has provided an invalid username and password combination</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(Token), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(BadRequestResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [SwaggerResponseExample((int)HttpStatusCode.Unauthorized, typeof(UnauthorizedResponseExample))]
        public async Task<IActionResult> Authenticate([FromBody, Required] LoginCredentials loginCredentials)
        {
            if (loginCredentials is not null)
            {
                var user = await _userManager.FindByNameAsync(loginCredentials.Username);

                if (user is not null && await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    var roleClaims = userRoles.Select(r => new Claim(ClaimTypes.Role, r));
                    claims.AddRange(roleClaims);

                    var token = GetToken(claims);

                    return Ok(new Token()
                    {
                        Value = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    });
                }
            }

            return Unauthorized();
        }

        private JwtSecurityToken GetToken(List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
