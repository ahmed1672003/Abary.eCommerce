using System.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace eCommerce.Domain.Abstractions.Contexts;

public interface IeCommerceDbContext : IAsyncDisposable, IDisposable, ICurrentDbContext
{
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct);
    Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken ct
    );
    Task<bool> EnsureCreatedAsync(CancellationToken ct = default);

    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;

    Task<bool> IsDoneAsync(int modifiedRows, CancellationToken ct);

    bool IsDone(int modifiedRows);
}
