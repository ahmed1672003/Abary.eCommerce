namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

public sealed record UpdateCategoryRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
