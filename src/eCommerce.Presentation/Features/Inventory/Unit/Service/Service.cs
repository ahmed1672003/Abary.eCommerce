using eCommerce.Presentation.Features.Inventory.Units.DaoService;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Units.Service;

public sealed class UnitService : IUnitService
{
    readonly IUnitDaoService _unitDaoService;

    public UnitService(IUnitDaoService unitDaoService) => _unitDaoService = unitDaoService;

    public Task<Response> CreateAsync(CreateUnitRequest request, CancellationToken ct) =>
        _unitDaoService.CreateAsync(request, ct);

    public Task<Response> UpdateAsync(UpdateUnitRequest request, CancellationToken ct) =>
        _unitDaoService.UpdateAsync(request, ct);

    public Task<Response> DeleteAsync(DeleteUnitRequest request, CancellationToken ct) =>
        _unitDaoService.DeleteAsync(request, ct);

    public Task<Response> GetAsync(GetUnitRequest request, CancellationToken ct) =>
        _unitDaoService.GetAsync(request, ct);

    public Task<Response> GetAllAsync(GetAllUnitsRequest request, CancellationToken ct) =>
        _unitDaoService.GetAllAsync(request, ct);
}
