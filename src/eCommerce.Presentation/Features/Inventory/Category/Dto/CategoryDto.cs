using eCommerce.Domain.Bases.Dto;

namespace eCommerce.Presentation.Features.Inventory.Categories.Dto;

public record CategoryDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
