using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;

internal sealed class CreatUnitValidator : Validator<CreateUnitRequest>
{
    public CreatUnitValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

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
            .MinimumLength(0)
            .WithMessage("غير مسموح بأقل من حرف")
            .When(req => !string.IsNullOrEmpty(req.Description));

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var _context = Resolve<IeCommerceDbContext>())
                    {
                        var _units = _context.Set<Unit>();

                        return !await _units.AnyAsync(x =>
                            x.Name.ToLower().Equals(req.Name.ToLower())
                        );
                    }
                }
            )
            .WithMessage("يوجد لديك وحدة بهذا الاسم");
    }
}
