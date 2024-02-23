using eCommerce.Domain.Enums;

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
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public string Value { get; set; }
    public string OldValue { get; set; }
    public EntityType EntityType { get; set; }
    public CommandType CommandType { get; set; }
    public FeatureName FeatureName { get; set; }
    public ModuleName ModuleName { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    #endregion
}
