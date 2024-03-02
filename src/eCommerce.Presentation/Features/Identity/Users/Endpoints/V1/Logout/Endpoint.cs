using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Logout;

internal sealed class LogoutUserEndpoint : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Identity)}/{nameof(FeatureName.User)}/{nameof(Logout)}");
        Permissions();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response = await Resolve<IUserService>().LogoutAsync(ct);
    }
}
