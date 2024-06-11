using JwtAuthentication.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication.Infrastructure.Services.Interface;

public interface ICustomAuthenticationService
{
    public bool ValidateUser(User user);
    public string GenerateToken(User validUser);
    public ClaimsPrincipal? ValidateToken(string token);
}

