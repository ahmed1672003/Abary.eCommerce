using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Presentation.Features.Inventory.Stocks;

#region Create
internal class CreateStockEndpoint : Endpoint<CreateStockRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Identity)}/Stock/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateStockRequest req, CancellationToken ct)
    {
        Response = new Response<StockDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new StockDto
            {
                ExpirationDate = req.ExpirationDate,
                Files = req.Files,
                ProductId = req.ProductId,
                PurchasePrice = req.PurchasePrice,
                Quantity = req.Quantity,
                VendorIds = req.VendorIds
            }
        };
    }
}

public class StockDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Guid> VendorIds { get; set; }
    public List<string> Files { get; set; }
}

public class CreateStockRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Guid> VendorIds { get; set; }
    public List<string> Files { get; set; }
}
#endregion

#region Delete

public class DeleteStockEndpoint : Endpoint<DeleteStockRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/Stock/Delete");
        Permissions();
    }

    public override async Task HandleAsync(DeleteStockRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteStockRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
#endregion

#region Update
internal class UpdateStockEndpoint : Endpoint<UpdateStockRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Identity)}/Stock/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateStockRequest req, CancellationToken ct)
    {
        Response = new Response<StockDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new StockDto
            {
                ExpirationDate = req.ExpirationDate,
                Files = req.Files,
                ProductId = req.ProductId,
                PurchasePrice = req.PurchasePrice,
                Quantity = req.Quantity,
                VendorIds = req.VendorIds
            }
        };
    }
}

public class UpdateStockRequest
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime ExpirationDate { get; set; }
    public List<Guid> VendorIds { get; set; }
    public List<string> Files { get; set; }
}
#endregion

#region Get
public class GetStockEndpoint : Endpoint<GetStockRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Identity)}/Stock/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetStockRequest req, CancellationToken ct)
    {
        Response = new Response<StockDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new StockDto
            {
                ProductId = req.Id,
                ExpirationDate = DateTime.Now.AddYears(1),
                Files = new List<string>()
                {
                    $"server/wwwroot/{Guid.NewGuid()}",
                    $"server/wwwroot/{Guid.NewGuid()}"
                },
                PurchasePrice = 100,
                Quantity = 30,
                VendorIds = new() { Guid.NewGuid() }
            }
        };
    }
}

public class GetStockRequest
{
    public Guid Id { get; set; }
}
#endregion
