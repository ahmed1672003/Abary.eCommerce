using System.Linq.Expressions;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Domain.Enums.Inventory.Stocks;
using eCommerce.Presentation.Features.Inventory.Stocks.DaoService;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Service;

public sealed class StockService : IStockService
{
    readonly IStockDaoService _stockDaoService;

    public StockService(IStockDaoService stockDaoService) => _stockDaoService = stockDaoService;

    public Task<Response> CreateAsync(CreateStockRequest request, CancellationToken ct) =>
        _stockDaoService.CreateAsync(request, ct);

    public Task<Response> UpdateAsync(UpdateStockRequest request, CancellationToken ct) =>
        _stockDaoService.UpdateAsync(request, ct);

    public Task<Response> DeleteAsync(DeleteStockRequest request, CancellationToken ct) =>
        _stockDaoService.DeleteAsync(request, ct);

    public Task<Response> GetAsync(GetStockRequest request, CancellationToken ct) =>
        _stockDaoService.GetAsync(request, ct);

    public Task<Response> GetAllAsync(GetAllStocksRequest request, CancellationToken ct)
    {
        Expression<Func<Stock, object>> orderBy = request.OrderBy switch
        {
            StockOrderBy.Id => x => x.Id,
            StockOrderBy.Name => x => x.Name,
            StockOrderBy.CreatedOn => x => x.CreatedOn,
            _ => (Stock x) => x.CreatedOn
        };
        return _stockDaoService.GetAllAsync(request, orderBy, ct);
    }
}
