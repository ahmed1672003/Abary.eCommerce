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
            if (entry is not { State: EntityState.Added, Entity: ITrackableCreate<Guid> entity, })
                continue;

            await entity.CreateAsync();
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Modified, Entity: ITrackableUpdate<Guid> entity })
                continue;

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
            await entity.DeleteAsync();
        }

        return result;
    }
}
