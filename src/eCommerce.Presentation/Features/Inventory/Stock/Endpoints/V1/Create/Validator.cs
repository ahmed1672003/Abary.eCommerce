using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;

internal sealed class CreateStockValidator : Validator<CreateStockRequest>
{
    public CreateStockValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("اسم المخزن مطلوب")
            .NotEmpty()
            .WithMessage("اسم المخزن مطلوب");

        RuleFor(x => x.Address.Country)
            .NotEmpty()
            .WithMessage("اسم الدولة مطلوب")
            .NotNull()
            .WithMessage("اسم الدولة مطلوب")
            .When(req => req.Address != null);

        RuleFor(x => x.Address.City)
            .NotEmpty()
            .WithMessage("اسم المدينة مطلوب")
            .NotNull()
            .WithMessage("اسم المدينة مطلوب")
            .When(req => req.Address != null);

        RuleFor(x => x.Name).Length(1, 100).WithMessage("(1,100) عدد الحروف يجب أن يتراوح بين ");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var context = Resolve<IeCommerceDbContext>())
                    {
                        return !await context
                            .Set<Stock>()
                            .AsNoTracking()
                            .AnyAsync(x => x.Name.ToLower().Equals(req.Name.ToLower()), ct);
                    }
                }
            )
            .WithMessage("يوجد مخزن مسجل بنفس الاسم من قبل");
    }
}
