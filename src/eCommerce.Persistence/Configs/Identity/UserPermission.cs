namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable(EntityName.UserPermission.ToString(), ModuleName.Identity.ToString());
        builder.HasKey(x => new { x.PermissionId, x.UserId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
