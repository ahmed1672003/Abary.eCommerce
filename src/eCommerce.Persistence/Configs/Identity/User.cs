namespace eCommerce.Persistence.Configs.Identity;

internal sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(EntityName.User.ToString(), ModuleName.Identity.ToString());
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.UserName).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
