using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;

internal sealed class DeleteUnitValidator : Validator<DeleteUnitRequest>
{
    public DeleteUnitValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).NotNull().WithMessage("حدث خطأ").NotEmpty().WithMessage("حدث خطأ");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using var _context = Resolve<IeCommerceDbContext>();

                    var _units = _context.Set<Unit>();

                    return await _units.AsNoTracking().AnyAsync(x => x.Id == req.Id);
                }
            )
            .WithMessage("الوحدة غير موجودة");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using var _context = Resolve<IeCommerceDbContext>();

                    var _units = _context.Set<Unit>();

                    return await _units
                        .AsNoTracking()
                        .Include(x => x.Products)
                        .AnyAsync(x => x.Id == req.Id && !x.Products.Any());
                }
            )
            .WithMessage("لايمكن حذف وحدة مرتبطة بمنتجات");
    }
}
