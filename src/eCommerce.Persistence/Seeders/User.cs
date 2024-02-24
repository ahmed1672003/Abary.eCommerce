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
        Console.WriteLine("Users Seeded Success....");
    }

    public static async Task SeedDeveloperAsync(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct = default
    )
    {
        var permissions = context
            .Set<Permission>()
            .Select(x => new UserPermission { PermissionId = x.Id, });

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

        await userManager.CreateAsync(developer, SystemConstants.Developer.PASSWORD);
    }
}
