using System.ComponentModel.DataAnnotations;

namespace JwtAuthentication.Domain;

public class User
{
    [Display(Name = "UserId")]
    public string Email { get; set; } = string.Empty; // consider this as Email

    public string Name { get; set; } = string.Empty;

    public string? Password { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

}
