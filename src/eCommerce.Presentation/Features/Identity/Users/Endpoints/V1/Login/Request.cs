namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

public sealed record LoginUserRequest
{
    public string EmailOrUserName { get; set; }
    public string Pssword { get; set; }
}
