using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Services.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;

internal sealed class GetAllServicesEndpoint : Endpoint<GetAllServicesRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Service)}/{nameof(GetAll)}");
        Permissions(SystemConstants.Security.Inventory.Services.GetAll);
    }

    public override async Task HandleAsync(GetAllServicesRequest req, CancellationToken ct) =>
        Response = await Resolve<IServiceService>().GetAllAsync(req, ct);
}
