using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public interface IUserService
{
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
}
