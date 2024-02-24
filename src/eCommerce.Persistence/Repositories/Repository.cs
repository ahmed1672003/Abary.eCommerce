namespace eCommerce.Persistence.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    #region Fields
    protected readonly IeCommerceDbContext _context;
    readonly DbSet<TEntity> _entities;
    #endregion

    #region Ctor
    public Repository(IeCommerceDbContext context)
    {
        _context = context;
        _entities = _context.Set<TEntity>();
    }
    #endregion

    #region Commands
    public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        try
        {
            var entry = await _entities.AddAsync(entity, ct);
            return entry.Entity;
        }
        catch (Exception ex)
        {
            throw new CreateEntityException(ex.Message);
        }
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        try
        {
            var entry = _entities.Update(entity);
            return Task.FromResult(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new DatabaseConflictException(ex.Message);
        }
    }

    public virtual Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        try
        {
            var entry = _entities.Remove(entity);
            return Task.FromResult(entry.Entity);
        }
        catch (Exception ex)
        {
            throw new DatabaseConflictException(ex.Message);
        }
    }
    #endregion

    #region Queries

    public virtual async Task<TEntity> GetAsync(
        ISpecification<TEntity> specification,
        CancellationToken ct = default
    )
    {
        try
        {
            var query = SpecificationEvaluator.Query(_entities, specification);

            return await query.FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new DatabaseExecuteQueryException(ex.Message, ex);
        }
    }

    public virtual Task<IQueryable<TEntity>> GetAllAsync(
        ISpecification<TEntity> specification = null,
        CancellationToken ct = default
    )
    {
        try
        {
            var query = SpecificationEvaluator.Query(_entities, specification);

            return Task.FromResult(query);
        }
        catch (Exception ex)
        {
            throw new DatabaseExecuteQueryException(ex.Message, ex);
        }
    }

    public virtual Task<int> CountAsync(
        ISpecification<TEntity> specification = null,
        CancellationToken ct = default
    )
    {
        try
        {
            if (specification is not null && specification is not { Criteria: null })
                return _entities.CountAsync(specification.Criteria, ct);
            else
                return _entities.CountAsync(ct);
        }
        catch (Exception ex)
        {
            throw new DatabaseExecuteQueryException(ex.Message, ex);
        }
    }
    #endregion
}
