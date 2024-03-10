using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable(nameof(EntityName.ProductCategory), nameof(ModuleName.Inventory));
        builder.HasKey(x => new { x.ProductId, x.CategoryId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
