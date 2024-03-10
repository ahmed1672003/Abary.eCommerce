using System.Linq.Expressions;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Domain.Enums.Inventory.Services;
using eCommerce.Presentation.Features.Inventory.Services.DaoService;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Services.Services;

public sealed class ServiceService : IServiceService
{
    readonly IServiceDaoService _serviceDaoService;

    public ServiceService(IServiceDaoService serviceDaoService) =>
        _serviceDaoService = serviceDaoService;

    public Task<Response> CreateAsync(CreateServiceRequest request, CancellationToken ct) =>
        _serviceDaoService.CreateAsync(request, ct);

    public Task<Response> UpdateAsync(UpdateServiceRequest request, CancellationToken ct) =>
        _serviceDaoService.UpdateAsync(request, ct);

    public Task<Response> DeleteAsync(DeleteServiceRequest request, CancellationToken ct) =>
        _serviceDaoService.DeleteAsync(request, ct);

    public Task<Response> GetAsync(GetServiceRequest reqyuest, CancellationToken ct) =>
        _serviceDaoService.GetAsync(reqyuest, ct);

    public Task<Response> GetAllAsync(GetAllServicesRequest request, CancellationToken ct)
    {
        Expression<Func<Service, object>> orderBy = request.OrderBy switch
        {
            ServiceOrderBy.Id => (Service service) => service.Id,
            ServiceOrderBy.Name => (Service service) => service.Name,
            ServiceOrderBy.Price => (Service service) => service.Price,
            _ => (Service service) => service.CreatedOn
        };

        return _serviceDaoService.GetAllAsync(request, orderBy, ct);
    }
}
