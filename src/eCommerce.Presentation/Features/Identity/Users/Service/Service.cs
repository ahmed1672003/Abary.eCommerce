using eCommerce.Presentation.Features.Identity.Users.DaoService;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public sealed class UserService : IUserService
{
    readonly IUserDaoService _userDaoService;

    public UserService(IUserDaoService userDaoService) => _userDaoService = userDaoService;

    public Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct) =>
        _userDaoService.CreatAsync(request, ct);

    public Task<Response> LoginAsync(LoginUserRequest reqest, CancellationToken ct) =>
        _userDaoService.LoginAsync(reqest, ct);

    public Task<Response> GetAsync(GetUserRequest request, CancellationToken ct) =>
        _userDaoService.GetAsync(request, ct);
}
