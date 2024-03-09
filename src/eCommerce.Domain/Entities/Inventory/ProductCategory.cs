namespace eCommerce.Domain.Entities.Inventory;

public sealed class ProductCategory
    : ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public ProductCategory() { }
    #endregion

    #region Keys
    public Guid ProductId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigation Props
    public Product Product { get; set; }
    public Category Category { get; set; }
    #endregion
}
