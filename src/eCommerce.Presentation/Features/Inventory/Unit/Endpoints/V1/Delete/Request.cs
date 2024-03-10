namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;

public sealed record DeleteUnitRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
