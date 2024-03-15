using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Stocks.Service;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;

internal sealed class DeleteStockEndpoint : Endpoint<DeleteStockRequest>
{
    public override void Configure()
    {
        Version(1);
        Delete($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Stock)}/{nameof(Delete)}");
        Permissions(SystemConstants.Security.Inventory.Stocks.Delete);
    }

    public override async Task HandleAsync(DeleteStockRequest req, CancellationToken ct) =>
        Response = await Resolve<IStockService>().DeleteAsync(req, ct);
}
