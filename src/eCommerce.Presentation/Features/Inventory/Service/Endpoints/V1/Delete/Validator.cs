using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;

internal sealed class DeleteServiceValidator : Validator<DeleteServiceRequest>
{
    public DeleteServiceValidator()
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
