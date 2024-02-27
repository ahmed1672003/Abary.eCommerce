using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Identity.Users.Service;
using FastEndpoints;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public sealed class CreateUserEndpoint : Endpoint<CreateUserRequest, Response>
{
    public override void Configure()
    {
        Post($"{ModuleName.Identity}/{FeatureName.User}/{nameof(Create)}");
        Version(1);
        Permissions(SystemConstants.Security.Identity.Users.Create);
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct) =>
        Response = await Resolve<IUserService>().CreatAsync(req, ct);
}
