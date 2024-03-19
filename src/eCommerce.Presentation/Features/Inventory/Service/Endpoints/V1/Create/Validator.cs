using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;

internal sealed class CreateServiceValidator : Validator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("اسم الخدمة مطلوب")
            .NotEmpty()
            .WithMessage("اسم الخدمة مطلوب");

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("سعر الخدمة مطلوب")
            .NotEmpty()
            .WithMessage("سعر الخدمة مطلوب");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(1)
            .WithMessage("يجب عليك ادخال سعر مناسب للخدمة")
            .LessThanOrEqualTo(decimal.MaxValue)
            .WithMessage("ادخل رقم صحيح");

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
                        var _services = _context.Set<Service>();

                        return !await _services
                            .AsNoTracking()
                            .AnyAsync(x => x.Name.ToLower() == req.Name.ToLower(), ct);
                    }
                }
            )
            .WithMessage("توجد خدمة بهذا الاسم");
    }
}
