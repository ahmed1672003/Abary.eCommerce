using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Services.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;

internal sealed class GetServiceEndpoint : Endpoint<GetServiceRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Service)}/{nameof(Get)}");
        Permissions(SystemConstants.Security.Inventory.Services.Get);
    }

    public override async Task HandleAsync(GetServiceRequest req, CancellationToken ct) =>
        Response = await Resolve<IServiceService>().GetAsync(req, ct);
}
