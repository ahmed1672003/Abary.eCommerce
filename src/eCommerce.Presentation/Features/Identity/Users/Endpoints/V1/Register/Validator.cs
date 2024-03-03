namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

internal sealed class RegisterUserValidator : Validator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage("اسم المستخدم مطلوب")
            .NotEmpty()
            .WithMessage("اسم المستخدم مطلوب");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("البريد الألكتروني مطلوب")
            .NotNull()
            .WithMessage("البريد الألكتروني مطلوب");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("كلمة المرور مطلوبة")
            .NotNull()
            .WithMessage("كلمة المرور مطلوبة");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("تأكيد كلمة المرور مطلوبة")
            .NotNull()
            .WithMessage("تأكيد كلمة المرور مطلوبة");

        RuleFor(x => x.Password)
            .Matches(x => x.ConfirmPassword)
            .WithMessage("كلمتا المرور غير متطابقتان");

        RuleFor(x => x.Email)
            .Must(email =>
            {
                return new EmailAddressAttribute().IsValid(email);
            })
            .WithMessage("البريد الألكتروني غير صحيح");

        RuleFor(x => x.Email)
            .Must(email =>
            {
                return new EmailAddressAttribute().IsValid(email);
            })
            .WithMessage("البريد الألكتروني غير صحيح");

        RuleFor(x => x.AuthProvider).IsInEnum().WithMessage("نوع خدمة تسجيل حساب غير صالحة");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var users = Resolve<IeCommerceDbContext>().Set<User>();

                    return !await users.AnyAsync(x => x.NormalizedEmail == req.Email.ToUpper(), ct);
                }
            )
            .WithMessage("البريد الألكتروني مسجل من قبل");

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
            )
            .WithMessage("اسم المستخدم مسجل من قبل");
    }
}
