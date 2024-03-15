namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;

public sealed record DeleteCategoryRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
