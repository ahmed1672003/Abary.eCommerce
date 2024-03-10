using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Units.Service;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;

public sealed class DeleteUnitEndpoint : Endpoint<DeleteUnitRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Unit)}/{nameof(Delete)}");
        Permissions(SystemConstants.Security.Inventory.Units.Delete);
    }

    public override async Task HandleAsync(DeleteUnitRequest req, CancellationToken ct) =>
        Response = await Resolve<IUnitService>().DeleteAsync(req, ct);
}
