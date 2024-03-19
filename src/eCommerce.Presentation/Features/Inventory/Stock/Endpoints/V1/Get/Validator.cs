using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;

internal sealed class GetStockValidator : Validator<GetStockRequest>
{
    public GetStockValidator()
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
                            .Set<Stock>()
                            .AsNoTracking()
                            .AnyAsync(x => x.Id == req.Id, ct);
                    }
                }
            )
            .WithMessage("المخزن غير مسجل");
    }
}
