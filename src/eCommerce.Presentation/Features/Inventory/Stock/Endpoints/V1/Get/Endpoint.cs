using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Stocks.Service;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;

internal sealed class GetStockEndpoint : Endpoint<GetStockRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Stock)}/{nameof(Get)}");
        Permissions(SystemConstants.Security.Inventory.Stocks.Get);
    }

    public override async Task HandleAsync(GetStockRequest req, CancellationToken ct) =>
        Response = await Resolve<IStockService>().GetAsync(req, ct);
}
