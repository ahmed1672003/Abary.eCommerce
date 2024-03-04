namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;

public sealed record AuthenticateUserRequest
{
    public string? EmailOrUserName { get; set; }

    public string? Pssword { get; set; }

    public LoginProvider? LoginProvider { get; set; }

    public string? RefreshToken { get; set; }
}
