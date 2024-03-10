namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Update;

public sealed record UpdateServiceRequest(Guid Id, string Name, string? Description, decimal Price);
