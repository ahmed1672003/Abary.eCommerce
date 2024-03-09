namespace eCommerce.Domain.Entities.Identity;

public sealed class Permission
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor
    public Permission()
    {
        Id = Guid.NewGuid();
        PermissionUsers = new(0);
    }
    #endregion

    #region Keys
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public EntityName Entity { get; set; }
    public ModuleName Module { get; set; }
    public string Value { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? UpdatedOn { get; set; }
    #endregion

    #region Navigations
    public List<UserPermission> PermissionUsers { get; set; }
    #endregion
}
