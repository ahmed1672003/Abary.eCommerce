﻿namespace eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;

public sealed record GetStockRequest
{
    public Guid Id { get; set; }
}
