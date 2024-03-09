namespace eCommerce.Presentation.Features.Inventory.Units.Dto;

public sealed class UnitDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedOn { get; set; }
}
