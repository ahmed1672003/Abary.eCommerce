namespace eCommerce.Domain.Entities.Inventory;

public sealed class StockProduct
    : ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    #endregion

    #region Keys

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    [ForeignKey(nameof(Stock))]
    public Guid StockId { get; set; }

    public int Qty { get; set; }
    public int Price { get; set; }

    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public int LimitStockToNotify { get; set; }

    [ForeignKey(nameof(Vendor))]
    public Guid VendorId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Navigation Props
    public Product Product { get; set; }
    public Stock Stock { get; set; }
    public Vendor Vendor { get; set; }
    #endregion
}
