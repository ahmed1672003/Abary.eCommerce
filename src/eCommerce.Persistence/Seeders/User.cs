namespace eCommerce.Persistence.Seeders;

public static class UserSeeder
{
    public static async Task SeedAsync(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct = default
    )
    {
        await SeedDeveloperAsync(context, userManager, ct);
    }

    private static async Task SeedDeveloperAsync(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct = default
    )
    {
        using var transaction = await context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var permissions = context
                .Set<Permission>()
                .Select(x => new UserPermission { PermissionId = x.Id, });

            modifiedRows += permissions.Count();

            User developer = new User
            {
                Email = SystemConstants.Developer.EMAIL,
                NormalizedEmail = SystemConstants.Developer.EMAIL.ToUpper(),
                UserName = SystemConstants.Developer.USER_NAME,
                NormalizedUserName = SystemConstants.Developer.USER_NAME.ToUpper(),
                PhoneNumber = SystemConstants.Developer.PHONE_NUMBER,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                UserPremissions = await permissions.ToListAsync(ct)
            };

            var success = await userManager.CreateAsync(
                developer,
                SystemConstants.Developer.PASSWORD
            );

            if (success.Succeeded)
            {
                await transaction.CommitAsync(ct);
                Console.WriteLine("Users Seeded Success....");
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
}
