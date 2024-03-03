namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;

public sealed class GetUserValidator : Validator<GetUserRequest>
{
    public GetUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var context = Resolve<IeCommerceDbContext>();
                    var users = context.Set<User>();
                    return await users.AsNoTracking().AnyAsync(x => x.Id == req.Id, ct);
                }
            )
            .WithMessage("المستخدم غير موجود");
    }
}
