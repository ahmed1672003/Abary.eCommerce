namespace eCommerce.Domain.Entities.Inventory;

public sealed class Product
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public Product()
    {
        ProductStocks = new(0);
        ProductFeatures = new(0);
        ProductCategories = new(0);
    }
    #endregion

    #region Keys
    [ForeignKey(nameof(Unit))]
    public Guid UnitId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public List<string> Files { get; set; }
    public string Barcode { get; set; }
    public string Currency { get; set; }
    public decimal Tax { get; set; }
    public List<string> Photos { get; set; }
    public decimal MaxDiscount { get; set; }
    public decimal PurchasePrice { get; set; }
    public decimal SalePrice { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigation Props
    public Unit Unit { get; set; }
    public List<ProductFeature> ProductFeatures { get; set; }
    public List<StockProduct> ProductStocks { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    #endregion
}
