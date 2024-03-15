using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Update;

internal sealed class UpdateUserEndpoint : Endpoint<UpdateUserRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Put($"{nameof(ModuleName.Identity)}/{nameof(FeatureName.User)}/{nameof(Update)}");
        Permissions(SystemConstants.Security.Identity.Users.Update + "UnderDevelopment");
    }

    public override async Task HandleAsync(UpdateUserRequest req, CancellationToken ct) =>
        Response = await Resolve<IUserService>().UpdateAsync(req, ct);
}
