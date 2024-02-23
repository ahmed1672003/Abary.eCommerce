namespace eCommerce.Presentation.Identity.Features.User.Service;

public sealed class UserService : IUserService
{
    public Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct) =>
        throw new NotImplementedException();
}
