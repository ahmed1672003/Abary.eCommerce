using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;

internal sealed class GetCategoryValidator : Validator<GetCategoryRequest>
{
    public GetCategoryValidator()
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
    }
}
