namespace eCommerce.Domain.Bases.Entities;

public class BaseEntity<TId>
{
    public BaseEntity(TId key) => Id = key;

    public BaseEntity() { }

    public TId Id { get; set; }
}
