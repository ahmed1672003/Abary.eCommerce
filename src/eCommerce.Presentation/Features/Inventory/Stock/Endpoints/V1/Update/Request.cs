namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Update;

public sealed record UpdateStockRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public UpdateAddressRequest? Address { get; set; }

    public sealed record UpdateAddressRequest
    {
        public string City { get; set; }
        public string Country { get; set; }
    }
}
