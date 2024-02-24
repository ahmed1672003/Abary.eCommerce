namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(EntityName.UserToken.ToString(), ModuleName.Identity.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
