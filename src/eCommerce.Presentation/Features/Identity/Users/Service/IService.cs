using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public interface IUserService
{
    Task<Response> RegisterAsync(RegisterUserRequest request, CancellationToken ct);
    Task<Response> LoginAsync(LoginUserRequest reqest, CancellationToken ct);
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetUserRequest request, CancellationToken ct);
    Task<Response> GetAllAsync(GetAllUsersRequest request, CancellationToken ct);
    Task<Response> LogoutAsync(CancellationToken ct);
}
