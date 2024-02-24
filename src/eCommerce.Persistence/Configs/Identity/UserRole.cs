namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(EntityName.UserRole.ToString(), ModuleName.Identity.ToString());
        builder.HasKey(x => new { x.UserId, x.RoleId });
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
