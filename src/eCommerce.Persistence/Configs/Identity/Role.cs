namespace eCommerce.Persistence.Configs.Identity;

internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(EntityName.Role.ToString(), ModuleName.Identity.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
