using eCommerce.Presentation.Features.Identity.Users.Service;
using FastEndpoints;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;

public sealed class LoginUserEndpoint : Endpoint<LoginUserRequest, Response>
{
    public override void Configure()
    {
        Get($"{ModuleName.Identity}/{EntityName.User}/{nameof(Login)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        Response = await Resolve<IUserService>().LoginAsync(req, ct);
    }
}
