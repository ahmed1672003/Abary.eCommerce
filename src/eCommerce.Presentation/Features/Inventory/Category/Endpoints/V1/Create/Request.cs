namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;

public sealed record CreateCategoryRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
