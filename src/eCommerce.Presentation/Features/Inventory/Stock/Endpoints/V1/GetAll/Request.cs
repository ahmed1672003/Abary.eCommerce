using eCommerce.Domain.Bases.Request;
using eCommerce.Domain.Enums.Inventory.Stocks;

namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.GetAll;

public sealed record GetAllStocksRequest(StockOrderBy OrderBy = StockOrderBy.CreatedOn)
    : PaginateRequest;
