namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;

public sealed record UpdateUnitRequest(Guid Id, string Name, string? Description);
