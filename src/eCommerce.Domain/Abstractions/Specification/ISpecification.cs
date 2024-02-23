using System.Linq.Expressions;

namespace eCommerce.Domain.Abstractions.Specification;

public interface ISpecification<TEntity>
    where TEntity : class
{
    Expression<Func<TEntity, bool>> Criteria { get; set; }

    List<Expression<Func<TEntity, object>>> Includes { get; set; }

    Expression<Func<TEntity, object>> OrderBy { get; set; }

    Expression<Func<TEntity, object>> OrderByDescending { get; set; }

    Expression<Func<TEntity, object>> GroupBy { get; set; }

    List<string> IncludesString { get; set; }

    int PageNumber { get; set; }

    int PageSize { get; set; }

    bool IsPagingEnabled { get; set; }

    bool IsTrackingOf { get; set; }

    bool IsQueryFilterIgnored { get; set; }
}
