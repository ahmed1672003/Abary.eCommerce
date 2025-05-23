﻿namespace eCommerce.Domain.Entities.Inventory;

public sealed class Stock
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public Stock()
    {
        StockProducts = new(0);
    }
    #endregion

    #region Keys
    [ForeignKey(nameof(Address))]
    public Guid? AddressId { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid DeletedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    #endregion

    #region Props
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    #endregion

    #region Navigation Props
    public Address Address { get; set; }
    public List<StockProduct> StockProducts { get; set; }
    #endregion
}
