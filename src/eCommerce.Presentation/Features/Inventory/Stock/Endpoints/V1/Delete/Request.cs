namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;

public sealed record DeleteStockRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
