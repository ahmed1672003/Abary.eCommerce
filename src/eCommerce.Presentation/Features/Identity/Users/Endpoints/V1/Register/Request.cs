namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

public record RegisterUserRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public LoginProvider AuthProvider { get; set; }
}
