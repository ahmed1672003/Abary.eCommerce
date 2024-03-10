namespace eCommerce.Domain.Entities.Inventory;

public sealed class Item
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public Item()
    {
        ItemServices = new(0);
    }
    #endregion

    #region Keys
    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(Invoice))]
    public Guid InvoiceId { get; set; }

    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigation Props
    public Invoice Invoice { get; set; }
    public Product Product { get; set; }
    public List<ItemService> ItemServices { get; set; }
    #endregion
}
