namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

internal sealed class RegisterUserValidator : Validator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName).NotNull().WithMessage(x => $"{x.UserName} ldskdjklsds");

        RuleFor(x => x.Email).NotEmpty().NotNull();

        RuleFor(x => x.Password).NotEmpty().NotNull();

        RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull();

        RuleFor(x => x.Password).Matches(x => x.ConfirmPassword);

        RuleFor(x => x.Email)
            .Must(email =>
            {
                return new EmailAddressAttribute().IsValid(email);
            });

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var users = Resolve<IeCommerceDbContext>().Set<User>();

                    return !await users.AnyAsync(x => x.NormalizedEmail == req.Email.ToUpper(), ct);
                }
            );

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var users = Resolve<IeCommerceDbContext>().Set<User>();

                    return !await users.AnyAsync(
                        x => x.NormalizedUserName == req.UserName.ToUpper(),
                        ct
                    );
                }
            );
    }
}
