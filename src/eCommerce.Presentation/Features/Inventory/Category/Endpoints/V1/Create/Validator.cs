using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;

internal sealed class CreateCategoryValidator : Validator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("اسم الصنف مطلوب")
            .NotEmpty()
            .WithMessage("اسم الصنف مطلوب");

        RuleFor(x => x.Name).Length(1, 100).WithMessage("عدد الحروف يجب أن يكون بحد أقصي 100 حرف");

        RuleFor(x => x.Description)
            .Length(0, 4000)
            .WithMessage("عدد الحروف يجب أن يكون بحد أقصي 4000 حرف")
            .When(req => !string.IsNullOrEmpty(req.Description));
        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var context = Resolve<IeCommerceDbContext>())
                    {
                        return !await context
                            .Set<Category>()
                            .AsNoTracking()
                            .AnyAsync(x => x.Name.ToLower().Equals(req.Name.ToLower()));
                    }
                }
            )
            .WithMessage("اسم الصنف مسجل من قبل");
    }
}
