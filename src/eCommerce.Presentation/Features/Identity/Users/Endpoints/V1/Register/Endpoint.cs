using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;

public class RegisterUserEndpoint : Endpoint<RegisterUserRequest, Response>
{
    public override void Configure()
    {
        Post($"{ModuleName.Identity}/{EntityName.User}/{nameof(Register)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        Response = await Resolve<IUserService>().RegisterAsync(req, ct);
    }
}
