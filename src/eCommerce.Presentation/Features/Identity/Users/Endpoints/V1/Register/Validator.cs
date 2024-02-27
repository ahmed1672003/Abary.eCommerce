using FastEndpoints;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

internal sealed class RegisterUserValidator : Validator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}
