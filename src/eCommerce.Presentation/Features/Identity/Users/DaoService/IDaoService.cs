using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

namespace eCommerce.Presentation.Features.Identity.Users.DaoService;

public interface IUserDaoService
{
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
}
