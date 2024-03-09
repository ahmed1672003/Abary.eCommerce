using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class ProductFeatureConfig : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.ToTable(nameof(EntityName.ProductFeature), nameof(ModuleName.Inventory));
        builder.HasKey(x => new { x.ProductId, x.FeatureId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
