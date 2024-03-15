using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public class CreateUserEndpoint : Endpoint<CreateUserRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Post($"{ModuleName.Identity}/{FeatureName.User}/{nameof(Create)}");
        Permissions(SystemConstants.Security.Identity.Users.Create);
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct) =>
        Response = await Resolve<IUserService>().CreatAsync(req, ct);
}
