namespace eCommerce.Persistence.Configs.Identity;

internal sealed class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(EntityName.Permission.ToString(), ModuleName.Identity.ToString());
        builder.HasKey(p => p.Id);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
