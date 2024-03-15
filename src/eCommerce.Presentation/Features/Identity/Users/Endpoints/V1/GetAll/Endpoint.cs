using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;

public class GetAllUsersEndpoint : Endpoint<GetAllUsersRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Identity)}/{nameof(FeatureName.User)}/{nameof(GetAll)}");
        Permissions(SystemConstants.Security.Identity.Users.GetAll);
    }

    public override async Task HandleAsync(GetAllUsersRequest req, CancellationToken ct)
    {
        Response = await Resolve<IUserService>().GetAllAsync(req, ct);
    }
}
