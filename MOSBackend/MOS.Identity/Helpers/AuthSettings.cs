namespace MOS.Identity.Helpers;

public class AuthSettings
{
    public string JwtSecretKey { get; set; } = "";
    public string AuthSalt { get; set; } = "";
}