using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Get;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;

internal sealed class GetUnitValidator : Validator<GetUnitRequest>
{
    public GetUnitValidator()
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
    }
}
