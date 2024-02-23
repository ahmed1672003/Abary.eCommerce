namespace eCommerce.Presentation.Identity.Features.User.Service;

public interface IUserService
{
    Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct);
}
