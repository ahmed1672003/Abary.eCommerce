using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(EntityName.Product), nameof(ModuleName.Inventory));
        builder.HasKey(x => x.Id);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
