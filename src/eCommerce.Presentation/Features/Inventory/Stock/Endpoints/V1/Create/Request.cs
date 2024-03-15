namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;

public sealed record CreateStockRequest
{
    public string Name { get; set; }
    public CreateAddressRequest? Address { get; set; }

    public sealed record CreateAddressRequest
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
