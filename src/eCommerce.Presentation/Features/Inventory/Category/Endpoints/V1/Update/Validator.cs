using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

internal sealed class UpdateCategoryValidator : Validator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

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
                        return await context
                            .Set<Category>()
                            .AsNoTracking()
                            .AnyAsync(x => x.Id.Equals(req.Id));
                    }
                }
            )
            .WithMessage("الصنف غير موجود");

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var context = Resolve<IeCommerceDbContext>())
                    {
                        return !await context
                            .Set<Category>()
                            .AsNoTracking()
                            .AnyAsync(x =>
                                x.Name.ToLower().Equals(req.Name.ToLower()) && !x.Id.Equals(req.Id)
                            );
                    }
                }
            )
            .WithMessage("يوجد صنف مسجل بنفس الاسم");
    }
}
