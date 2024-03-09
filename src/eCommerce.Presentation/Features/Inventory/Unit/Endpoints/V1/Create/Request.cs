namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;

public sealed record CreateUnitRequest
{
    public string Name { get; set; }
    public string Discreption { get; set; }
}
