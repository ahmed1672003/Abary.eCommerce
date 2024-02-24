using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

namespace eCommerce.Presentation.Features.Identity.Users.DaoService;

public interface IUserDaoService
{
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
    Task<Response> LoginAsync(LoginUserRequest request, CancellationToken ct);
}
