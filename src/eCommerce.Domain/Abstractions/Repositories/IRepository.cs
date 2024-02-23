namespace eCommerce.Domain.Abstractions.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    #region Commands
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default);
    #endregion

    #region Queries
    Task<TEntity> GetAsync(ISpecification<TEntity> specification, CancellationToken ct = default);

    Task<IQueryable<TEntity>> GetAllAsync(
        ISpecification<TEntity> specification = null,
        CancellationToken ct = default
    );

    Task<int> CountAsync(
        ISpecification<TEntity> specification = null,
        CancellationToken ct = default
    );
    #endregion
}
