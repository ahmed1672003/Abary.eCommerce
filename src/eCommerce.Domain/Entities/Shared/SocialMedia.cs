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
    public Guid? UserProfileId { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public List<string> Facebook { get; set; }
    public List<string> WhatsApp { get; set; }
    public List<string> Instagram { get; set; }
    public List<string> X { get; set; }
    public List<string> Threads { get; set; }
    public List<string> TikTock { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public UserProfile UserProfile { get; set; }
    #endregion
}
