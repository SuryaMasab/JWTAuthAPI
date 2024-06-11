namespace Authentication.Domain;

public class ValidateRequest
{
    public string Token { get; set; } = string.Empty;
    public string[] Claims { get; set; } = [];
}
