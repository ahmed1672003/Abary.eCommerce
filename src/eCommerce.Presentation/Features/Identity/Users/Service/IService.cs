using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public interface IUserService
{
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
    Task<Response> LoginAsync(LoginUserRequest reqest, CancellationToken ct);
    Task<Response> GetAsync(GetUserRequest request, CancellationToken ct);
}
