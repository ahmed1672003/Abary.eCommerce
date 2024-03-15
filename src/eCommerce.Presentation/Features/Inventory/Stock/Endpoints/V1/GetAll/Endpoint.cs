using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Stocks.Service;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.GetAll;

internal sealed class GetAllStocksEndpoint : Endpoint<GetAllStocksRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Stock)}/{nameof(GetAll)}");
        Permissions(SystemConstants.Security.Inventory.Stocks.GetAll);
    }

    public override async Task HandleAsync(GetAllStocksRequest req, CancellationToken ct) =>
        Response = await Resolve<IStockService>().GetAllAsync(req, ct);
}
