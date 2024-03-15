using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Units.Service;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;

public sealed class GetAllUnitsEndpoint : Endpoint<GetAllUnitsRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Unit)}/{nameof(GetAll)}");
        Permissions(SystemConstants.Security.Inventory.Units.GetAll);
    }

    public override async Task HandleAsync(GetAllUnitsRequest req, CancellationToken ct) =>
        Response = await Resolve<IUnitService>().GetAllAsync(req, ct);
}
