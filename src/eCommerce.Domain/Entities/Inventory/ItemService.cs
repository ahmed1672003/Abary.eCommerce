namespace eCommerce.Domain.Entities.Inventory;

public sealed class ItemService
    : ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public ItemService() { }
    #endregion

    #region Keys

    [ForeignKey(nameof(Service))]
    public Guid ServiceId { get; set; }

    [ForeignKey(nameof(Item))]
    public Guid ItemId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public string Name { get; set; }
    public string Value { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigation Props
    public Service Service { get; set; }
    public Item Item { get; set; }
    #endregion
}
