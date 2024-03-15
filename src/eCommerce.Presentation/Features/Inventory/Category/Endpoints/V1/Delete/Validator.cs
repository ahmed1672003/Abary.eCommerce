using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;

internal sealed class DeleteCategoryValidator : Validator<DeleteCategoryRequest>
{
    public DeleteCategoryValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

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
                            .Include(x => x.ProductCategories)
                            .AnyAsync(x => x.Id.Equals(req.Id) && x.ProductCategories.Any());
                    }
                }
            )
            .WithMessage("لايمكن حذف صنف تابع لمنتجات");
    }
}
