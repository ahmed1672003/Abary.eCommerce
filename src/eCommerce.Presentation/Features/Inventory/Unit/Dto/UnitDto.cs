using eCommerce.Domain.Bases.Dto;

namespace eCommerce.Presentation.Features.Inventory.Units.Dto;

public sealed record UnitDto : BaseDto<Guid>
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
