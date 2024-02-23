namespace eCommerce.Presentation.Identity.Features.User.Endpoints.V1.Create;

public sealed class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}
