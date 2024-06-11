using JwtAuthentication.Domain;
using JwtAuthentication.Infrastructure.Repositories.Interface;
using JwtAuthentication.Infrastructure.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Infrastructure.Services;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly string jwtSubject;
    private readonly string jwtIssuer;
    private readonly string jwtAudience;
    private readonly string jwtSecurityKey;
    private readonly IUserRepository _userRepository;

    public CustomAuthenticationService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        jwtSubject = _configuration["JwtToken:Subject"] ?? throw new ArgumentNullException("Missing JWT Subject!");
        jwtIssuer = _configuration["JwtToken:Issuer"] ?? throw new ArgumentNullException("Missing JWT Issuer!");
        jwtAudience = _configuration["JwtToken:Audience"] ?? throw new ArgumentNullException("Missing JWT Audience!");
        jwtSecurityKey = _configuration["JwtToken:Key"] ?? throw new ArgumentNullException("Missing JWT Key!");
        _userRepository = userRepository;
    }

    public bool ValidateUser(User user)
    {
        var validateUser = _userRepository.GetUser(user.Email);
        if (validateUser == null)
        {
            return false;
        }

        if (validateUser.Password != user.Password)
        {
            return false;
        }
        return true;
    }
    public string GenerateToken(User validUser)
    {
        //user is valid.. 
        var userClaims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub,jwtSubject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(nameof(User.Email),validUser.Email), // these are the claims validated in the ValidateToken method
                new Claim(nameof(User.Name),validUser.Name)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey));

        var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var userToken = new JwtSecurityToken(
                           issuer: jwtIssuer,
                           audience: jwtAudience,
                           claims: userClaims,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: signInCredentials);
        string tokenValue = new JwtSecurityTokenHandler().WriteToken(userToken);
        return tokenValue;
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey))
            };
            var result = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return result;
        }
        catch
        {
            return null;
        }
    }
}