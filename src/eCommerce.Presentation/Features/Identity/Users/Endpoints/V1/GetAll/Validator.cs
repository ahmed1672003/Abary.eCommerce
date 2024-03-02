namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;

internal sealed class GetAllUsersValidator : Validator<GetAllUsersRequest>
{
    public GetAllUsersValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}
