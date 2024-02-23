namespace eCommerce.Domain.Entities.Shared;

public sealed class Notification
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public Notification() => Id = Guid.NewGuid();
    #endregion

    #region Keys
    public Guid EntityId { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public string? EntityValue { get; set; }
    public string? EntityNewValue { get; set; }
    public string? EntityOldValue { get; set; }
    public string? RepositoryName { get; set; }
    public EntityName? Entity { get; set; }
    public CommandType? CommandType { get; set; }
    public FeatureName? Feature { get; set; }
    public ModuleName? Module { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    #endregion
}
