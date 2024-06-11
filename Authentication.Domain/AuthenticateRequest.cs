
namespace Authentication.Domain;

public class AuthenticateRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}