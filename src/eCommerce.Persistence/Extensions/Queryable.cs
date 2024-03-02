using eCommerce.Domain.Bases.Request;

namespace eCommerce.Presentation.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(
        this IQueryable<TEntity> query,
        PaginateRequest request,
        Expression<Func<TEntity, object>> orderBy
    )
    {
        query = query.Skip((request.Page - 1) * request.Size).Take(request.Size);

        if (orderBy != null && request.OrderByDirection == OrderByDirection.Ascending)
        {
            query = query.OrderBy(orderBy);
        }
        else
        {
            query = query.OrderByDescending(orderBy);
        }

        return query;
    }
}
