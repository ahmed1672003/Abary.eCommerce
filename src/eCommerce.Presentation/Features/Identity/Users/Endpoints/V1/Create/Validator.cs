using System.ComponentModel.DataAnnotations;
using eCommerce.Domain.Abstractions.Contexts;
using eCommerce.Domain.Entities.Identity;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public sealed class CreateUserValidator : Validator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Email).NotEmpty().NotEmpty();

        RuleFor(x => x.Password).NotEmpty().NotEmpty();

        RuleFor(x => x.ConfirmPassword).NotEmpty().NotEmpty();

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

                    return !await users.AnyAsync(x => x.Email == req.Email, ct);
                }
            );

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    var users = Resolve<IeCommerceDbContext>().Set<User>();

                    return !await users.AnyAsync(x => x.UserName == req.UserName, ct);
                }
            );
    }
}
