namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.ChangePassword;

public sealed record ChangePasswordRequest
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmNewPassword { get; set; }
}
