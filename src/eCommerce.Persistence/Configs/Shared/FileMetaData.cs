namespace eCommerce.Persistence.Configs.Shared;

public sealed class FileMetaDataConfig : IEntityTypeConfiguration<FileMetaData>
{
    public void Configure(EntityTypeBuilder<FileMetaData> builder)
    {
        builder.ToTable(EntityName.FilMetaData.ToString(), ModuleName.Shared.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
