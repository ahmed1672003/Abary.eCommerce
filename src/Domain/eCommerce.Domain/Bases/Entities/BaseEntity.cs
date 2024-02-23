namespace eCommerce.Domain.Bases.Entities;
public class BaseEntity<TKey>
{
    public BaseEntity(TKey key) => Key = key;

    public TKey Key { get; set; }
}
