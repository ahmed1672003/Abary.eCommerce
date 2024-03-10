namespace eCommerce.Domain.Entities.Identity;

public sealed class UserRole
    : IdentityUserRole<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor

    #endregion

    #region Keys
    [ForeignKey(nameof(User))]
    public override Guid UserId { get; set; }

    [ForeignKey(nameof(Role))]
    public override Guid RoleId { get; set; }

    public Guid CreatedBy { get; set; } = Guid.Empty;

    public Guid DeletedBy { get; set; } = Guid.Empty;

    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public User User { get; set; }

    public Role Role { get; set; }
    #endregion
}
