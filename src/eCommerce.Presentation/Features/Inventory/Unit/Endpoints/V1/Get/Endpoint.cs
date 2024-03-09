using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Units.Service;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Get;

public sealed class GetUnitEndpoint : Endpoint<GetUnitRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Unit)}/{nameof(Get)}");
        Permissions(SystemConstants.Security.Inventory.Units.Get);
    }

    public override async Task HandleAsync(GetUnitRequest req, CancellationToken ct) =>
        Response = await Resolve<IUnitService>().GetAsync(req, ct);
}
