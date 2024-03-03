using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

public sealed class LoginUserValidator : Validator<AuthenticateUserRequest>
{
    public LoginUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.EmailOrUserName)
            .NotEmpty()
            .WithMessage("الحقل مطلوب")
            .NotNull()
            .WithMessage("الحقل مطلوب")
            .When(x => x.RefreshToken == null && x.EmailOrUserName != null);

        RuleFor(x => x.Pssword)
            .NotEmpty()
            .WithMessage("الحقل مطلوب")
            .NotNull()
            .WithMessage("الحقل مطلوب")
            .When(x => x.RefreshToken == null && x.Pssword != null);

        RuleFor(x => x.LoginProvider)
            .IsInEnum()
            .WithMessage("نوع خدمة تسجيل الدخول غير صالحة")
            .When(x => x.RefreshToken == null && x.LoginProvider.HasValue);

        RuleFor(x => x)
            .Must(req => !(IsRefreshTokenRequestValid(req) && IsLoginRequestValid(req)))
            .WithMessage("عيب اللي بتعمله دا ينقاش");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await Resolve<IeCommerceDbContext>()
                        .Set<UserToken>()
                        .AsNoTracking()
                        .Include(x => x.User)
                        .AnyAsync(x =>
                            x.RefreshToken == req.RefreshToken && x.User != null && x.IsRevoked
                        );
                }
            )
            .WithMessage("من فضلك أعد تسجيل الدخول")
            .When(IsRefreshTokenRequestValid);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var userManager = Resolve<UserManager<User>>();
                    var emailOrUserName = req.EmailOrUserName.ToUpper();

                    var user = new EmailAddressAttribute().IsValid(req.EmailOrUserName)
                        ? await userManager.Users.FirstOrDefaultAsync(
                            x => x.NormalizedEmail == emailOrUserName,
                            ct
                        )
                        : await userManager.Users.FirstOrDefaultAsync(x =>
                            x.NormalizedUserName == emailOrUserName
                        );

                    return user != null && await userManager.CheckPasswordAsync(user, req.Pssword);
                }
            )
            .WithMessage("البيانات غير صحيحة")
            .When(IsLoginRequestValid);
    }

    bool IsLoginRequestValid(AuthenticateUserRequest request) =>
        request.RefreshToken == null
        && request.EmailOrUserName != null
        && request.Pssword != null
        && request.LoginProvider.HasValue;

    bool IsRefreshTokenRequestValid(AuthenticateUserRequest request) =>
        request.RefreshToken != null
        && request.EmailOrUserName == null
        && !request.LoginProvider.HasValue;
}
