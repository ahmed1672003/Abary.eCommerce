using eCommerce.Domain.Bases.Entities;

namespace eCommerce.Persistence.Interceptors;

public sealed class CustomSaveChangesInterceptor : SaveChangesInterceptor
{
    public sealed override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        if (eventData.Context is null)
            return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: ISoftDeleteable entity })
                continue;

            if (entity.IsDeleted)
            {
                await entity.UndoSoftDeleteAsync();
            }
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: UserToken entity })
                continue;

            if (
                entity.RevokedOn != null
                || entity.CreatedOn.AddSeconds(entity.ExpiresIn) >= DateTime.UtcNow
            )
            {
                entity.IsRevoked = true;
            }
            else
            {
                entity.IsRevoked = false;
            }
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Added, Entity: ITrackableCreate<Guid> entity })
                continue;

            if (entity.CreatedBy == Guid.Empty)
            {
                entity.CreatedBy = Guid.Parse(SystemConstants.SYSTEM_KEY);
            }
            await entity.CreateAsync();
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Added, Entity: BaseEntity<Guid> entity, })
                continue;

            entity.Id = Guid.NewGuid();
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Added, Entity: UserToken entity })
                continue;

            if (
                entity.RevokedOn != null
                || entity.CreatedOn.AddSeconds(entity.ExpiresIn) >= DateTime.UtcNow
            )
            {
                entity.IsRevoked = true;
            }
            else
            {
                entity.IsRevoked = false;
            }
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: ITrackableUpdate<Guid> entity })
                continue;

            if (entity == null || entity.UpdatedBy == Guid.Empty)
            {
                entity.UpdatedBy = Guid.Parse(SystemConstants.SYSTEM_KEY);
            }

            await entity.UpdateAsync();
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDeleteable entity })
                continue;
            if (!entity.IsDeleted)
            {
                await entity.DeleteAsync();
            }
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ITrackableDelete<Guid> entity })
                continue;

            entry.State = EntityState.Modified;

            if (entity.DeletedBy == Guid.Empty)
            {
                entity.DeletedBy = Guid.Parse(SystemConstants.SYSTEM_KEY);
            }

            await entity.DeleteAsync();
        }

        return result;
    }
}
