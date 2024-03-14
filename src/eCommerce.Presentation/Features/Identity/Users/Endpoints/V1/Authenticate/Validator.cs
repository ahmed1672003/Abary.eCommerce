using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

public sealed class AuthenticateUserValidator : Validator<AuthenticateUserRequest>
{
    public AuthenticateUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(req => (IsRefreshTokenRequestValid(req)))
            .When(req => !IsLoginRequestValid(req))
            .WithMessage("invalid reequest");

        RuleFor(x => x)
            .Must(req => (IsLoginRequestValid(req)))
            .When(req => !IsRefreshTokenRequestValid(req))
            .WithMessage("invalid reequest");

        RuleFor(x => x.EmailOrUserName)
            .NotEmpty()
            .WithMessage("is required")
            .NotNull()
            .WithMessage("is required")
            .When(x => x.RefreshToken == null && x.EmailOrUserName != null);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("is required")
            .NotNull()
            .WithMessage("is required")
            .When(x => x.RefreshToken == null && x.Password != null);

        RuleFor(x => x.LoginProvider)
            .IsInEnum()
            .WithMessage("login provider is not valid")
            .When(x => x.RefreshToken == null && x.LoginProvider.HasValue);

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await Resolve<IeCommerceDbContext>()
                        .Set<UserToken>()
                        .AsNoTracking()
                        .AnyAsync(x => x.RefreshToken == req.RefreshToken && x.IsRevoked);
                }
            )
            .WithMessage("login again")
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

                    return user != null && await userManager.CheckPasswordAsync(user, req.Password);
                }
            )
            .WithMessage("incorrect data")
            .When(IsLoginRequestValid);
    }

    bool IsLoginRequestValid(AuthenticateUserRequest request) =>
        request.RefreshToken == null
        && request.EmailOrUserName != null
        && request.Password != null
        && request.LoginProvider.HasValue;

    bool IsRefreshTokenRequestValid(AuthenticateUserRequest request) =>
        request.RefreshToken != null
        && request.EmailOrUserName == null
        && !request.LoginProvider.HasValue;
}
