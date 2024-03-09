using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;

internal sealed class UpdateUnitValidator : Validator<UpdateUnitRequest>
{
    public UpdateUnitValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).NotNull().WithMessage("حدث خطأ").NotEmpty().WithMessage("حدث خطأ");

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("اسم الوحدة مطلوب")
            .NotEmpty()
            .WithMessage("اسم الوحدة مطلوب");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("غير مسموح بأكثر من 100 حرف")
            .MinimumLength(1)
            .WithMessage("غير مسموح بأقل من حرف");

        RuleFor(x => x.Description)
            .MaximumLength(3000)
            .WithMessage("غير مسموح بأكثر من 3000 حرف")
            .MaximumLength(1)
            .WithMessage("غير مسموح بأقل من حرف")
            .When(req => !string.IsNullOrEmpty(req.Description));

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

                    return !await _units
                        .AsNoTracking()
                        .AnyAsync(x =>
                            x.Name.ToLower().Equals(req.Name.ToLower()) && x.Id != req.Id
                        );
                }
            )
            .WithMessage("يوجد لديك وحدة بهذا الاسم");
    }
}
