namespace eCommerce.Persistence.Settings;

public class JwtSettings
{
    public string Secret => Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
    public string Issuer => Environment.GetEnvironmentVariable("JWT_ISSURE");
    public string Audience => Environment.GetEnvironmentVariable("JWT_AUDIENCE");

    public bool ValidateIssuer =>
        Convert.ToBoolean(Environment.GetEnvironmentVariable("JWT_VALIDATE_AUDIENCE"));

    public bool ValidateAudience =>
        Convert.ToBoolean(Environment.GetEnvironmentVariable("JWT_VALIDATE_ISSUER"));

    public bool ValidateLifeTime =>
        Convert.ToBoolean(Environment.GetEnvironmentVariable("JWT_VALIDATE_LIFETIME"));

    public bool ValidateIssuerSigningKey =>
        Convert.ToBoolean(Environment.GetEnvironmentVariable("JWT_VALIDATE_ISSUER_SIGN_IN_KEY"));

    public long AccessTokenExpireDate =>
        Convert.ToInt64(Environment.GetEnvironmentVariable("JWT_ACCESS_TOKEN_EXPIRES_IN"));
}
