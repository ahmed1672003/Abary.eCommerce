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
            var sharedPermissions = GeneratePermissions(ModuleName.Shared, EntityName.FilMetaData);

            // Identity
            var identityPermissions = GeneratePermissions(ModuleName.Identity, EntityName.User);

            // Inventory
            var inventoryPermissions = GeneratePermissions(ModuleName.Inventory, EntityName.Unit)
                .Union(GeneratePermissions(ModuleName.Inventory, EntityName.Invoice))
                .Union(GeneratePermissions(ModuleName.Inventory, EntityName.Category))
                .Union(GeneratePermissions(ModuleName.Inventory, EntityName.Product))
                .Union(GeneratePermissions(ModuleName.Inventory, EntityName.Service))
                .Union(GeneratePermissions(ModuleName.Inventory, EntityName.Stock));

            var seededPermissions = sharedPermissions
                .Union(identityPermissions)
                .Union(inventoryPermissions);

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
