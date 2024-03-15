using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;
using eCommerce.Presentation.Features.Inventory.Units.Service;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;

public sealed class UpdateUnitEndpoint : Endpoint<UpdateUnitRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Put($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Unit)}/{nameof(Update)}");
        Permissions(SystemConstants.Security.Inventory.Units.Update);
    }

    public override async Task HandleAsync(UpdateUnitRequest req, CancellationToken ct) =>
        Response = await Resolve<IUnitService>().UpdateAsync(req, ct);
}
