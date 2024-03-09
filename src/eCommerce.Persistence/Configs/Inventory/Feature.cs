using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class FeatureConfig : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable(nameof(EntityName.Feature), nameof(ModuleName.Inventory));
        builder.HasKey(x => x.Id);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
