namespace eCommerce.Persistence.Configs.Shared;

public sealed class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(EntityName.Address.ToString(), ModuleName.Shared.ToString());

        builder
            .HasOne(x => x.UserProfile)
            .WithOne(x => x.Address)
            .HasForeignKey<UserProfile>(x => x.AddressId);

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
