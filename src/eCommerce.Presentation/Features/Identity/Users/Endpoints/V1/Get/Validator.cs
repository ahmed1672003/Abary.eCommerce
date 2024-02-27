using eCommerce.Domain.Abstractions.Contexts;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Exceptions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

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
                    if (await users.AsNoTracking().AnyAsync(x => x.Id == req.Id, ct))
                    {
                        return true;
                    }
                    else
                    {
                        throw new NotFoundException("user not found");
                    }
                }
            );
    }
}
