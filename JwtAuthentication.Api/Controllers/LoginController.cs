using Authentication.Domain;
using JwtAuthentication.Api.Helpers;
using JwtAuthentication.Domain;
using JwtAuthentication.Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthService;

        public LoginController(ICustomAuthenticationService customAuthService)
        {
            _customAuthService = customAuthService;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Login(AuthenticateRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (GeoUtils.IsWithinAllowedDistance(request.Latitude, request.Longitude)) //validate the user location
            {
                // proceed with user validation
                var user = new User
                {
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Name = request.UserId, //using same field for name
                    Email = request.UserId,
                    Password = request.Password
                };
                var isAValidUser = _customAuthService.ValidateUser(user);

                if (isAValidUser)
                {
                    //Generate Token
                    var token = _customAuthService.GenerateToken(user);
                    return Ok(new { token });
                }

            }

            return Unauthorized();
        }


        [HttpPost]
        [Route("validate")]
        // [Authorize]
        public IActionResult ValidateUserToken(ValidateRequest request)
        {
            //validate token signature and claims and return Ok() if valid

            try
            {
                // Validate token
                var principal = _customAuthService.ValidateToken(request.Token);
                if (principal == null)
                {
                    return Ok(new { isValid = false });
                }

                // Check required claims
                var claimsValid = request.Claims.All(claim => principal.HasClaim(c => c.Type == claim));

                return Ok(new { isValid = claimsValid });
            }
            catch
            {
                return Ok(new { isValid = false });
            }
        }


    }
}
