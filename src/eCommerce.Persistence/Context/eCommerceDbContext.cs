﻿using System.Data;
using System.Reflection;

namespace eCommerce.Persistence.Context;

public sealed class eCommerceDbContext
    : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>,
        IeCommerceDbContext
{
    public eCommerceDbContext(DbContextOptions<eCommerceDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken ct) =>
        Database.BeginTransactionAsync(ct);

    public async Task<bool> IsDoneAsync(int modifiedRows, CancellationToken ct) =>
        await SaveChangesAsync(ct) == modifiedRows;

    public bool IsDone(int modifiedRows) => SaveChanges() == modifiedRows;

    public Task<bool> EnsureCreatedAsync(CancellationToken ct = default) =>
        this.Database.EnsureCreatedAsync(ct);

    public Task<IDbContextTransaction> BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken ct
    ) => Database.BeginTransactionAsync(isolationLevel, ct);

    public DbContext Context => this;
}
