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
    public string Name { get; set; }
    public List<string> PhotoUrls { get; set; }
    public double PurchasePrice { get; set; }
    public double SellingPrice { get; set; }
    public double Discount { get; set; } = 0;
    public double Tax { get; set; } = 0;
    public bool IsTaxPercentage { get; set; }
    public bool IsDiscountPercentage { get; set; }
    public bool AllowDiscount { get; set; }
    public bool AllowTax { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    #endregion

    #region Navigation Props
    public Unit Unit { get; set; }
    public List<ProductFeature> ProductFeatures { get; set; }
    public List<StockProduct> ProductStocks { get; set; }
    public List<ProductCategory> ProductCategories { get; set; }
    #endregion
}
