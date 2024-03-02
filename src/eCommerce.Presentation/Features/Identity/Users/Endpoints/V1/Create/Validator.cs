namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public sealed class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("حقل البريد الالكتروني مطلوب")
            .NotNull()
            .WithMessage("حقل البريد الالكتروني مطلوب");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("حقل اسم المستخدم مطلوب")
            .NotNull()
            .WithMessage("حقل اسم المستخدم مطلوب");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("كلمة المرور مطلوبة")
            .NotNull()
            .WithMessage("اسم المستخدم مطلوب");

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
