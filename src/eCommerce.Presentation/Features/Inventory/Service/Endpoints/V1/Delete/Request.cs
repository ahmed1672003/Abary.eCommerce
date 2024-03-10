namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;

public sealed record DeleteServiceRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
