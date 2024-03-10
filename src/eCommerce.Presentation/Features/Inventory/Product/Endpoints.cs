using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.HttpSys;

namespace eCommerce.Presentation.Features.Inventory.Products;

#region Create
internal class CreateProductEndpoint : Endpoint<CreateProductRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Identity)}/Product/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        Response = new Response<ProductDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new ProductDto
            {
                Id = Guid.NewGuid(),
                Barcode = req.Barcode,
                CategoryId = req.CategoryId,
                Currency = req.Currency,
                Description = req.Description,
                LimitStockToNotify = req.LimitStockToNotify,
                MaxDiscount = req.MaxDiscount,
                Name = req.Name,
                SellPrice = req.SellPrice,
                Tax = req.Tax,
                UnitIds = req.UnitIds,
            }
        };
    }
}

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> UnitIds { get; set; }
    public string Name { get; set; }
    public decimal MaxDiscount { get; set; }
    public int LimitStockToNotify { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal SellPrice { get; set; }
    public string Currency { get; set; }
    public string Tax { get; set; }
}

public class CreateProductRequest
{
    public Guid CategoryId { get; set; }
    public List<Guid>? UnitIds { get; set; }
    public string? Name { get; set; }
    public decimal MaxDiscount { get; set; }
    public int LimitStockToNotify { get; set; }
    public string Barcode { get; set; }
    public string? Description { get; set; }
    public decimal SellPrice { get; set; }
    public string Currency { get; set; }
    public string Tax { get; set; }
}
#endregion

#region Delete

public class DeleteProductEndpoint : Endpoint<DeleteProductRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Identity)}/Product/Delete");
        Permissions();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteProductRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
#endregion

#region Update
internal class UpdateProductEndpoint : Endpoint<UpdateProductRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Identity)}/Product/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        Response = new Response<ProductDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new ProductDto
            {
                Id = Guid.NewGuid(),
                Barcode = req.Barcode,
                CategoryId = req.CategoryId,
                Currency = req.Currency,
                Description = req.Description,
                LimitStockToNotify = req.LimitStockToNotify,
                MaxDiscount = req.MaxDiscount,
                Name = req.Name,
                SellPrice = req.SellPrice,
                Tax = req.Tax,
                UnitIds = req.UnitIds,
            }
        };
    }
}

public class UpdateProductRequest
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public List<Guid> UnitIds { get; set; }
    public string Name { get; set; }
    public decimal MaxDiscount { get; set; }
    public int LimitStockToNotify { get; set; }
    public string Barcode { get; set; }
    public string Description { get; set; }
    public decimal SellPrice { get; set; }
    public string Currency { get; set; }
    public string Tax { get; set; }
}
#endregion

#region Get
public class GetProductEndpoint : Endpoint<GetProductRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Identity)}/Product/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        Response = new Response<ProductDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new ProductDto
            {
                Id = Guid.NewGuid(),
                Barcode = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9",
                CategoryId = Guid.NewGuid(),
                Currency = Guid.NewGuid().ToString(),
                Description = "description",
                LimitStockToNotify = 10,
                MaxDiscount = 30,
                Name = "bike",
                SellPrice = 500,
                Tax = "4",
                UnitIds = new() { Guid.NewGuid(), Guid.NewGuid() },
            }
        };
    }
}

public class GetProductRequest
{
    public Guid Id { get; set; }
}
#endregion
