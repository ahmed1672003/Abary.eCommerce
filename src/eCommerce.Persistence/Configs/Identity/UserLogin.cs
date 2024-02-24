namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserLoginConfig : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable(EntityName.UserLogin.ToString(), ModuleName.Identity.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
