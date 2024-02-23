namespace eCommerce.Domain.Entities.Identity;

public sealed class Role
    : IdentityRole<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public Role()
    {
        Id = Guid.NewGuid();
        Claims = new(0);
        RoleUsers = new(0);
    }
    #endregion

    #region Keys
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public List<RoleClaim> Claims { get; set; }
    public List<UserRole> RoleUsers { get; set; }
    #endregion
}
