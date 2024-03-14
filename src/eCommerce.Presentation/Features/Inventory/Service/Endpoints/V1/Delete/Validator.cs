using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;

internal sealed class DeleteServiceValidator : Validator<DeleteServiceRequest>
{
    public DeleteServiceValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        using var _context = Resolve<IeCommerceDbContext>();
        var _services = _context.Set<Service>();

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    return await _services.AsNoTracking().AnyAsync(x => x.Id.Equals(req.Id), ct);
                }
            )
            .WithMessage("الوحدة غير موجودة");
    }
}
