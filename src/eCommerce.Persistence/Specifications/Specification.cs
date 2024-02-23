namespace eCommerce.Persistence.Specifications;

public class Specification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    public Specification() => IsTrackingOf = true;

    public List<Expression<Func<TEntity, object>>> Includes { get; set; } =
        new List<Expression<Func<TEntity, object>>>(0);

    public List<string> IncludesString { get; set; } = new List<string>(0);

    public Expression<Func<TEntity, bool>> Criteria { get; set; }

    public Expression<Func<TEntity, object>> OrderBy { get; set; }

    public Expression<Func<TEntity, object>> OrderByDescending { get; set; }

    public Expression<Func<TEntity, object>> GroupBy { get; set; }

    public bool IsPagingEnabled { get; set; }

    public bool IsTrackingOf { get; set; }

    public bool IsQueryFilterIgnored { get; set; }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
