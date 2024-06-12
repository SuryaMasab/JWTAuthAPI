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
                var loggedInUser = _customAuthService.ValidateUser(user);

                if (loggedInUser!=null)
                {
                    //Generate Token
                    var token = _customAuthService.GenerateToken(loggedInUser);
                    return Ok(new { token });
                }

            }

            return Unauthorized();
        }


        [HttpPost]
        [Route("validate")]
        // [Authorize]
        public IActionResult ValidateMobileUserToken(ValidateRequest request)
        {
            //validate token signature and claims

            try
            {
                // Validate token
                var principal = _customAuthService.ValidateToken(request.Token);
                if (principal == null)
                {
                    return Ok(new { isValid = false });
                }

                var claims = principal.Claims;
                if (claims == null)
                {
                    return Ok(new { isValid = false });
                }
                else
                {
                    var isAMobileUser = claims.Any(c => c.Type == nameof(JwtAuthentication.Domain.User.IsMobileUser) && c.Value == "true");

                    return Ok(new { isValid = isAMobileUser });
                } 
            }
            catch
            {
                return Ok(new { isValid = false });
            }
        }


    }
}
