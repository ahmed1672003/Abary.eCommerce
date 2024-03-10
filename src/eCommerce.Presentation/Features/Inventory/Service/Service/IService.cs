using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Services.Services;

public interface IServiceService
{
    Task<Response> CreateAsync(CreateServiceRequest request, CancellationToken ct);
    Task<Response> UpdateAsync(UpdateServiceRequest request, CancellationToken ct);
    Task<Response> DeleteAsync(DeleteServiceRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetServiceRequest reqyuest, CancellationToken ct);
    Task<Response> GetAllAsync(GetAllServicesRequest request, CancellationToken ct);
}
