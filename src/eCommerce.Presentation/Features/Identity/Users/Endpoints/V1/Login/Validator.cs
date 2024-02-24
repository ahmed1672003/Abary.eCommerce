using System.ComponentModel.DataAnnotations;
using eCommerce.Domain.Entities.Identity;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

public sealed class LoginUserValidator : Validator<LoginUserRequest>
{
    public LoginUserValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Pssword).NotEmpty().NotNull();

        RuleFor(x => x.Pssword).NotEmpty().NotEmpty();

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var userManager = Resolve<UserManager<User>>();

                    var user = new EmailAddressAttribute().IsValid(req.EmailOrUserName)
                        ? await userManager.Users.FirstOrDefaultAsync(
                            x => x.Email == req.EmailOrUserName,
                            ct
                        )
                        : await userManager.Users.FirstOrDefaultAsync(x =>
                            x.UserName == req.EmailOrUserName
                        );

                    return user != null && await userManager.CheckPasswordAsync(user, req.Pssword);
                }
            );
    }
}
