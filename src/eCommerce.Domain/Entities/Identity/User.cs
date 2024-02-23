namespace eCommerce.Domain.Entities.Identity;

public sealed class User
    : IdentityUser<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public User()
    {
        Id = Guid.NewGuid();
        UserRoles = new(0);
        Logins = new(0);
    }
    #endregion

    #region Keys
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    #endregion

    #region Props
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public UserProfile Profile { get; set; }
    public List<UserRole> UserRoles { get; set; }
    public List<UserLogin> Logins { get; set; }
    public UserToken Token { get; set; }
    #endregion
}
