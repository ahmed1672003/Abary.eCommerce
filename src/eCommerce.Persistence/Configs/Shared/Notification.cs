namespace eCommerce.Persistence.Configs.Shared;

public sealed class NotificationConfig : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(EntityName.Notification.ToString(), ModuleName.Shared.ToString());
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}
