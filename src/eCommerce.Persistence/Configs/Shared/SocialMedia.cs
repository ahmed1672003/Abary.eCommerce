namespace eCommerce.Persistence.Configs.Shared;

public sealed class SocialMediaConfig : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {
        builder.ToTable(EntityName.SocialMedia.ToString(), ModuleName.Shared.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
