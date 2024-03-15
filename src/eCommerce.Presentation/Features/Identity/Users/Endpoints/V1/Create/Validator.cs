namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("حقل اسم المستخدم مطلوب")
            .NotNull()
            .WithMessage("حقل اسم المستخدم مطلوب");

        RuleFor(x => x.UserName)
            .Must(userName =>
            {
                return !new EmailAddressAttribute().IsValid(userName);
            })
            .WithMessage("اسم المستخدم غير مسموح به");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("حقل البريد الألكتروني مطلوب")
            .NotNull()
            .WithMessage("حقل البريد الألكتروني مطلوب");

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

        RuleFor(x => x.Profile.FirstName)
            .NotEmpty()
            .WithMessage("من فضلك ادخل الأسم الأول")
            .NotNull()
            .WithMessage("من فضلك ادخل الأسم الأول")
            .When(req => req.Profile != null);

        RuleFor(x => x.Profile.LastName)
            .NotEmpty()
            .WithMessage("الاسم الأول مطلوب")
            .NotNull()
            .WithMessage("الاسم الأخير مطلوب")
            .When(req => req.Profile != null);

        RuleFor(x => x.Profile.Address.Country)
            .NotEmpty()
            .WithMessage("اسم الدولة مطلوب")
            .NotNull()
            .WithMessage("اسم الدولة مطلوب")
            .When(req => req.Profile != null && req.Profile.Address != null);

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
                        x => x.NormalizedUserName.Equals(req.UserName.ToUpper()),
                        ct
                    );
                }
            )
            .WithMessage("اسم المستخدم مسجل من قبل");
    }
}
