namespace eCommerce.Domain.Entities.Shared;

public sealed class SocialMedia
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public SocialMedia() => Id = Guid.NewGuid();
    #endregion

    #region Keys
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Props
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    #endregion

    #region Navigations

    #endregion
}
