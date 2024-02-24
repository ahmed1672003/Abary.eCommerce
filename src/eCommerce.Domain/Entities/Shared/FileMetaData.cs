namespace eCommerce.Domain.Entities.Shared;

[Table(nameof(EntityName.FilMetaData), Schema = nameof(ModuleName.Shared))]
[PrimaryKey(nameof(Id))]
public sealed class FileMetaData
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ISoftDeleteable,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>
{
    #region Ctor

    #endregion

    #region Keys
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public Guid DeletedBy { get; set; } = Guid.Empty;
    public Guid UpdatedBy { get; set; } = Guid.Empty;
    #endregion

    #region Props
    public long Size { get; set; }
    public string Extension { get; set; }
    public string Url { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigations

    #endregion
}
