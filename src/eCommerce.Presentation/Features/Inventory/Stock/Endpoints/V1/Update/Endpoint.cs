using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Stocks.Service;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Update;

internal sealed class UpdateStockEndpoint : Endpoint<UpdateStockRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Put($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Stock)}/{nameof(Update)}");
        Permissions(SystemConstants.Security.Inventory.Stocks.Update);
    }

    public override async Task HandleAsync(UpdateStockRequest req, CancellationToken ct) =>
        Response = await Resolve<IStockService>().UpdateAsync(req, ct);
}
