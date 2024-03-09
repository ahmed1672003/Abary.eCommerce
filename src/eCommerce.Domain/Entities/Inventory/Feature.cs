namespace eCommerce.Domain.Entities.Inventory;

public sealed class Feature
    : BaseEntity<Guid>,
        ITrackableCreate<Guid>,
        ITrackableDelete<Guid>,
        ITrackableUpdate<Guid>,
        ISoftDeleteable
{
    #region Ctor
    public Feature()
    {
        FeatureProducts = new(0);
    }
    #endregion

    #region Keys
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
    public List<ProductFeature> FeatureProducts { get; set; }
    #endregion
}
