using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;

internal sealed class DeleteStockValidator : Validator<DeleteStockRequest>
{
    public DeleteStockValidator()
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

        RuleFor(x => x)
            .MustAsync(
                async (req, ct) =>
                {
                    using (var context = Resolve<IeCommerceDbContext>())
                    {
                        return !await context
                            .Set<Stock>()
                            .AsNoTracking()
                            .Include(x => x.StockProducts)
                            .AnyAsync(x => x.Id == req.Id && x.StockProducts.Any(), ct);
                    }
                }
            )
            .WithMessage("أفرغ المخزن قبل الحذف");
    }
}
