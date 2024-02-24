namespace eCommerce.Persistence.Builders;

public sealed class NotificationBuilder
{
    private Notification _notification = new();

    public NotificationBuilder WithEntityId(Guid entityKey)
    {
        _notification.EntityId = entityKey;
        return this;
    }

    public NotificationBuilder WithServiceName(string serviceName)
    {
        _notification.ServiceName = serviceName;
        return this;
    }

    public NotificationBuilder WithEntityValue(string entityValue)
    {
        _notification.EntityValue = entityValue;
        return this;
    }

    public NotificationBuilder WithEntityOldValue(string oldValue)
    {
        _notification.EntityOldValue = oldValue;
        return this;
    }

    public NotificationBuilder WithEntityNewValue(string newValue)
    {
        _notification.EntityValue = newValue;
        return this;
    }

    public NotificationBuilder WithEntity(EntityName entity)
    {
        _notification.Entity = entity;
        return this;
    }

    public NotificationBuilder WithModule(ModuleName module)
    {
        _notification.Module = module;
        return this;
    }

    public NotificationBuilder WithFeature(FeatureName feature)
    {
        _notification.Feature = feature;
        return this;
    }

    public NotificationBuilder WithCommandType(CommandType commandType)
    {
        _notification.CommandType = commandType;
        return this;
    }

    public NotificationBuilder WithCreatedBy(Guid createdBy)
    {
        _notification.CreatedBy = createdBy;
        return this;
    }

    public NotificationBuilder WithUpdatedBy(Guid updtedBy)
    {
        _notification.CreatedBy = updtedBy;
        return this;
    }

    public NotificationBuilder Reset()
    {
        _notification = new();
        return this;
    }

    public Notification Build()
    {
        return _notification;
    }
}
