namespace eCommerce.Domain.Entities.Inventory;

public sealed class Service
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public Service()
    {
        SerivceItems = new(0);
    }
    #endregion

    #region Keys
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
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
    public List<ItemService> SerivceItems { get; set; }

    #endregion
}
