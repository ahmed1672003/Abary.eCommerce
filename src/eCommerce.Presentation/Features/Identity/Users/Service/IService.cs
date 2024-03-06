using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.ChangePassword;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public interface IUserService
{
    Task<Response> RegisterAsync(RegisterUserRequest request, CancellationToken ct);
    Task<Response> AuthenticateAsync(AuthenticateUserRequest reqest, CancellationToken ct);
    Task<Response> LogoutAsync(CancellationToken ct);
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
    Task<Response> UpdateAsync(UpdateUserRequest request, CancellationToken ct);
    Task<Response> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetUserRequest request, CancellationToken ct);
    Task<Response> GetAllAsync(GetAllUsersRequest request, CancellationToken ct);
}
