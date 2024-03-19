using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;

internal sealed class GetServiceValidator : Validator<GetServiceRequest>
{
    public GetServiceValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var _context = Resolve<IeCommerceDbContext>())
                    {
                        var _services = _context.Set<Service>();
                        return await _services.AsNoTracking().AnyAsync(x => x.Id == req.Id, ct);
                    }
                }
            )
            .WithMessage("الخدمة غير موجودة");
    }
}
