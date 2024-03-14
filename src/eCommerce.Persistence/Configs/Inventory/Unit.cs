using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class UnitConfig : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ToTable(nameof(EntityName.Unit), nameof(ModuleName.Inventory));
        builder.HasKey(e => e.Id);
        builder.HasIndex(x => x.Name).IsUnique(true);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
