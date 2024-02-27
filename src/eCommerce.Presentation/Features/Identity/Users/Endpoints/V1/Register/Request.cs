namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

public sealed record RegisterUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
}
