namespace eCommerce.Domain.Abstractions.Contexts;

public interface IeCommerceDbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct);

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<bool> IsDoneAsync(int modifiedRows, CancellationToken ct);

    bool IsDone(int modifiedRows);

    ValueTask DisposeAsync();
}
