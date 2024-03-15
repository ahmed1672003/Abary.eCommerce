using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;

public class GetUserEndpoint : Endpoint<GetUserRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{ModuleName.Identity}/{FeatureName.User}/{nameof(Get)}");
        Permissions(SystemConstants.Security.Identity.Users.Get);
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        Response = await Resolve<IUserService>().GetAsync(req, ct);
    }
}
