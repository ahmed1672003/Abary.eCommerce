namespace Pavon.Persistence.Builders;

public sealed class SpecificationBuilder<TEntity>
    where TEntity : class
{
    private ISpecification<TEntity> _specification = new Specification<TEntity>();

    public SpecificationBuilder<TEntity> Reset()
    {
        _specification = new Specification<TEntity>();
        return this;
    }

    public SpecificationBuilder<TEntity> HasCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        _specification.Criteria = criteria;
        return this;
    }

    public SpecificationBuilder<TEntity> HasGroupingBy(Expression<Func<TEntity, object>> groupBy)
    {
        _specification.GroupBy = groupBy;
        return this;
    }

    public SpecificationBuilder<TEntity> HasOrderBy(Expression<Func<TEntity, object>> orderBy)
    {
        _specification.OrderBy = orderBy;
        return this;
    }

    public SpecificationBuilder<TEntity> HasOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescending
    )
    {
        _specification.OrderByDescending = orderByDescending;
        return this;
    }

    public SpecificationBuilder<TEntity> HasIncludeString(string includeString)
    {
        _specification.IncludesString.Add(includeString);
        return this;
    }

    public SpecificationBuilder<TEntity> HasInclude(Expression<Func<TEntity, object>> include)
    {
        _specification.Includes.Add(include);
        return this;
    }

    public SpecificationBuilder<TEntity> HasGroupBy(Expression<Func<TEntity, object>> groupBy)
    {
        _specification.GroupBy = groupBy;
        return this;
    }

    public SpecificationBuilder<TEntity> HasPagination(int pageNumber, int pageSize)
    {
        _specification.PageSize = pageSize;
        _specification.PageNumber = pageNumber;
        _specification.IsPagingEnabled = true;

        return this;
    }

    public SpecificationBuilder<TEntity> HasNoTracking(bool stopTracking = true)
    {
        _specification.IsTrackingOf = stopTracking;
        return this;
    }

    public SpecificationBuilder<TEntity> HasNoQueryFilter(bool stopQueryFilter)
    {
        _specification.IsQueryFilterIgnored = stopQueryFilter;

        return this;
    }

    public ISpecification<TEntity> Build()
    {
        return _specification;
    }
}
