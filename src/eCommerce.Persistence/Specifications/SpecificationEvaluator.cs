namespace eCommerce.Persistence.Specifications;

public sealed class SpecificationEvaluator
{
    public static IQueryable<TEntity> Query<TEntity>(
        IQueryable<TEntity> source,
        ISpecification<TEntity> specification
    )
        where TEntity : class
    {
        if (specification is null)
            return source;

        IQueryable<TEntity> query = source;

        if (specification is { IsTrackingOf: true })
            query = query.AsNoTracking();

        if (specification is { IsQueryFilterIgnored: true })
            query = query.IgnoreQueryFilters();

        if (specification is not { Criteria: null })
            query = query.Where(specification.Criteria);

        if (specification is { Includes.Count: > 0 })
            query = specification.Includes.Aggregate(
                query,
                (current, include) => current.Include(include)
            );

        if (specification is { IncludesString.Count: > 0 })
            query = specification.IncludesString.Aggregate(
                query,
                (current, includeString) => current.Include(includeString)
            );

        if (specification is not { OrderBy: null })
            query = query.OrderBy(specification.OrderBy);

        if (specification is not { OrderByDescending: null })
            query = query.OrderByDescending(specification.OrderByDescending);

        if (specification is { IsPagingEnabled: true })
        {
            query = query
                .Skip((specification.PageNumber - 1) * specification.PageSize)
                .Take(specification.PageSize);
        }

        return query;
    }
}
