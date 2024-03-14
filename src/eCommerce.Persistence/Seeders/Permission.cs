namespace eCommerce.Persistence.Seeding;

public static class PermissionSeeder
{
    public static async Task<int> SeedPermissionsAsync(
        IeCommerceDbContext context,
        CancellationToken ct = default
    )
    {
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

        await permissions.AddRangeAsync(seededPermissions);

        return seededPermissions.Count();
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
