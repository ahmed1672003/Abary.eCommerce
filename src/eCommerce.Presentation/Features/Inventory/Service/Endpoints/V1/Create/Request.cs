namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;

public sealed record CreateServiceRequest(string Name, string? Description, decimal Price);
