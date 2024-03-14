using eCommerce.Domain.Constants;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;

internal sealed class CreateStockEndpoint : Endpoint<CreateStockRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Stock)}/{nameof(Create)}");
        Version(1);
        Permissions(SystemConstants.Security.Inventory.Stocks.Create);
    }
}
