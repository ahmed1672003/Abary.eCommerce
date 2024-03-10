namespace eCommerce.Presentation.Features.Inventory.Services.Dto;

public class ServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedOn { get; set; }
}
