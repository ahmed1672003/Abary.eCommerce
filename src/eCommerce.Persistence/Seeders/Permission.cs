namespace eCommerce.Persistence.Seeding;

public static class PermissionSeeder
{
    public static async Task SeedPermissionsAsync(
        IeCommerceDbContext context,
        CancellationToken ct = default
    )
    {
        using var transaction = await context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;
            var permissions = context.Set<Permission>();

            // shared
            var fileMetaDataPermissions = GeneratePermissions(
                ModuleName.Shared,
                EntityName.FilMetaData
            );

            // Identity
            var userPermissions = GeneratePermissions(ModuleName.Identity, EntityName.User);

            // Inventory
            var unitPermissions = GeneratePermissions(ModuleName.Inventory, EntityName.Unit);

            var seededPermissions = fileMetaDataPermissions
                .Union(userPermissions)
                .Union(unitPermissions);

            modifiedRows += seededPermissions.Count();
            await permissions.AddRangeAsync(seededPermissions);

            var success = await context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);
            }
            await transaction.RollbackAsync(ct);
            throw new DatabaseTransactionException();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw new DatabaseTransactionException(ex.Message, ex.InnerException);
        }
    }

    public static List<Permission> GeneratePermissions(ModuleName module, EntityName entity)
    {
        List<Permission> permissions = new(0);

        var permissionValues = Enum.GetValues<PermissionValue>();

        foreach (var value in permissionValues)
            permissions.Add(
                new()
                {
                    Value = $"{module}.{entity}.{value}",
                    Module = module,
                    Entity = entity
                }
            );
        return permissions;
    }
}
