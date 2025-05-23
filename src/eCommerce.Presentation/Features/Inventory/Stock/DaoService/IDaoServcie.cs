﻿using System.Linq.Expressions;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Stocks.DaoService;

public interface IStockDaoService
{
    Task<Response> CreateAsync(CreateStockRequest request, CancellationToken ct);
    Task<Response> UpdateAsync(UpdateStockRequest request, CancellationToken ct);
    Task<Response> DeleteAsync(DeleteStockRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetStockRequest request, CancellationToken ct);
    Task<Response> GetAllAsync(
        GetAllStocksRequest request,
        Expression<Func<Stock, object>> orderBy,
        CancellationToken ct
    );
}
