using eCommerce.Domain.Entities.Inventory;

namespace eCommerce.Persistence.Configs.Inventory;

internal sealed class ServiceConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable(nameof(EntityName.Service), nameof(ModuleName.Inventory));
        builder.HasKey(x => x.Id);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
