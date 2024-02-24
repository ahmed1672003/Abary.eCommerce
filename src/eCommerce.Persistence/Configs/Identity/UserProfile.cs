namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.ToTable(EntityName.UserProfile.ToString(), ModuleName.Identity.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
