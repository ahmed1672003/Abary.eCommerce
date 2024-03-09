using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Units.Service;

public interface IUnitService
{
    Task<Response> CreateAsync(CreateUnitRequest request, CancellationToken ct);
    Task<Response> UpdateAsync(UpdateUnitRequest request, CancellationToken ct);
    Task<Response> DeleteAsync(DeleteUnitRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetUnitRequest request, CancellationToken ct);
    Task<Response> GetAllAsync(GetAllUnitsRequest request, CancellationToken ct);
}
