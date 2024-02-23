namespace eCommerce.Persistence.Context;

public sealed class eCommerceDbContext
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IeCommerceDbContext
{
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct) =>
        Database.BeginTransactionAsync(ct);

    public async Task<bool> IsDoneAsync(int modifiedRows, CancellationToken ct) =>
        await SaveChangesAsync(ct) == modifiedRows;

    public bool IsDone(int modifiedRows) => SaveChanges() == modifiedRows;
}
