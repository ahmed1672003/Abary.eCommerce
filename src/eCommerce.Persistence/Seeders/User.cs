namespace eCommerce.Persistence.Seeders;

public class UserSeeder
{
    public static async Task<int> SeedAsync(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct = default
    )
    {
        return await SeedDeveloperAsync(context, userManager, ct);
    }

    private static async Task<int> SeedDeveloperAsync(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct = default
    )
    {
        List<User> users = new(0);

        users.Add(await PrepareDeveloper(context, userManager, ct));

        await context.Set<User>().AddRangeAsync(users);

        var modifiedRows = users.Count();

        foreach (var user in users)
        {
            modifiedRows += user.UserPremissions.Count();
        }

        return modifiedRows;
    }

    static async Task<User> PrepareDeveloper(
        IeCommerceDbContext context,
        UserManager<User> userManager,
        CancellationToken ct
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

        developer.PasswordHash = userManager.PasswordHasher.HashPassword(
            developer,
            SystemConstants.Developer.PASSWORD
        );

        return developer;
    }
}
