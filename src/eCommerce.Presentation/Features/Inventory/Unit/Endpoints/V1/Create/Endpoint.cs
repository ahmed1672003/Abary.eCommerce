using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Units.Service;

namespace eCommerce.Presentation.Features.Inventory.Unit.Endpoints.V1.Create;

public sealed class CreateUnitEndpoint : Endpoint<CreateUnitRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Unit)}/{CommandType.Create}");
        Permissions(SystemConstants.Security.Inventory.Units.Create);
    }

    public override async Task HandleAsync(CreateUnitRequest req, CancellationToken ct) =>
        Response = await Resolve<IUnitService>().CreateAsync(req, ct);
}
