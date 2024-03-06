namespace eCommerce.Domain.Entities.Identity;

[Table(name: nameof(EntityName.UserLogin), Schema = nameof(ModuleName.Identity))]
[PrimaryKey(nameof(LoginProvider), nameof(ProviderKey), nameof(UserId))]
public sealed class UserLogin
    : IdentityUserLogin<Guid>,
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
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public DateTime? EndLogin { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public User User { get; set; }
    #endregion
}
