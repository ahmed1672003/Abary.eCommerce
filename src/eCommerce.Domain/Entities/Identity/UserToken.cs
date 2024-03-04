using eCommerce.Domain.Enums.User;

namespace eCommerce.Domain.Entities.Identity;

[Table(nameof(EntityName.UserToken), Schema = nameof(ModuleName.Identity))]
[PrimaryKey(nameof(Id))]
public sealed class UserToken
    : IdentityUserToken<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public UserToken()
    {
        Id = Guid.NewGuid();
    }
    #endregion

    #region Keys

    public Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public override Guid UserId { get; set; }
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public AuthSchema Schema { get; set; }
    public string RefreshToken { get; set; }
    public long ExpiresIn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations
    public User User { get; set; }
    #endregion
}
