namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

public sealed class UpdateCategoryRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
}
