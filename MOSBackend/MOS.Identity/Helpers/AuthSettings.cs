namespace MOS.Identity.Helpers;

public class AuthSettings
{
    public string JwtSecretKey { get; set; } = "";
    public string AuthSalt { get; set; } = "";
    public int TokenDurationMinutes { get; set; } = 60;
}