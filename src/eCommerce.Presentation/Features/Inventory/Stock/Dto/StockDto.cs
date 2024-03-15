using eCommerce.Domain.Bases.Dto;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Dto;

public sealed record StockDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public AddressDto Address { get; set; }

    public record AddressDto
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
