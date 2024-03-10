using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class StockProductConfig : IEntityTypeConfiguration<StockProduct>
{
    public void Configure(EntityTypeBuilder<StockProduct> builder)
    {
        builder.ToTable(nameof(EntityName.StockProduct), nameof(ModuleName.Inventory));
        builder.HasKey(x => new { x.ProductId, x.StockId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
