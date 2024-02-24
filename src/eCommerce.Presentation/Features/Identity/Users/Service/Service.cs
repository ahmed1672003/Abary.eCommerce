using eCommerce.Presentation.Features.Identity.Users.DaoService;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public sealed class UserService : IUserService
{
    readonly IUserDaoService _userDaoService;

    public UserService(IUserDaoService userDaoService) => _userDaoService = userDaoService;

    public Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct)
    {
        return _userDaoService.CreatAsync(request, ct);
    }
}
