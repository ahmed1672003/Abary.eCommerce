using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class ItemServiceConfig : IEntityTypeConfiguration<ItemService>
{
    public void Configure(EntityTypeBuilder<ItemService> builder)
    {
        builder.ToTable(nameof(EntityName.ItemService), nameof(ModuleName.Inventory));
        builder.HasKey(x => new { x.ServiceId, x.ItemId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
