namespace eCommerce.Persistence.Seeding;

public static class PermissionSeeder
{
    public static async Task SeedPermissionsAsync(
        IeCommerceDbContext context,
        CancellationToken ct = default
    )
    {
        using var transaction = await context.BeginTransactionAsync(ct);
        var permissions = context.Set<Permission>();

        // shared
        var fileMetaDataPermissions = GeneratePermissions(
            ModuleName.Shared,
            EntityName.FilMetaData
        );

        // Identity
        var userPermissions = GeneratePermissions(ModuleName.Identity, EntityName.User);
        var seedingPermissions = fileMetaDataPermissions.Union(userPermissions);
        await permissions.AddRangeAsync(fileMetaDataPermissions.Union(userPermissions));
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
