namespace eCommerce.Persistence.Seeding;

public static class PermissionSeeder
{
    public static async Task SeedPermissionsAsync(
        IeCommerceDbContext context,
        CancellationToken ct = default
    )
    {
        var transaction = await context.BeginTransactionAsync(ct);
        try
        {
            var permissions = context.Set<Permission>();

            // shared
            var fileMetaDataPermissions = GeneratePermissions(
                ModuleName.Shared,
                EntityName.FilMetaData
            );

            // Identity
            var userPermissions = GeneratePermissions(ModuleName.Identity, EntityName.User);

            var seedingPermissions = fileMetaDataPermissions.Union(userPermissions);
            permissions.AddRange(fileMetaDataPermissions.Union(userPermissions));

            var result = await context.IsDoneAsync(seedingPermissions.Count(), ct);

            if (result)
            {
                await transaction.CommitAsync(ct);

                Console.WriteLine("Permissions Seeded Success....");
                return;
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
