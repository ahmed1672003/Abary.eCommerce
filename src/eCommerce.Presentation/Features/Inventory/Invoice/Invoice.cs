using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Presentation.Features.Inventory.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eCommerce.Presentation.Features.Inventory.Invoice;

#region Create
internal class CreateInvoiceEndpoint : Endpoint<CreateInvocieRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Inventory)}/Invocie/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateInvocieRequest req, CancellationToken ct)
    {
        var cost = 10;
        var taxPrec = 3;

        int tax = (taxPrec * 100) / taxPrec;

        var totalCost = req.InvoiceItems.Sum(x => x.Quantity) * cost;

        var total = totalCost * req.InvoiceItems.Sum(x => x.Quantity);

        var totalDiscount = req.InvoiceItems.Sum(x => x.Discount);

        var profit = total - totalCost - totalDiscount - totalCost + 0.2;

        Response = new Response<InviceDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new InviceDto
            {
                ContactInfo = null,
                Id = null,
                CreatedOn = DateTime.UtcNow,
                CreatorName = "ahmed",
                InvoiceProfit = Convert.ToDecimal(profit),
                InvoiceCost = Convert.ToDecimal(totalCost),
                InvoiceTotalAmount = Convert.ToDecimal(total),
                No = 100,
                TotalDiscountAmount = Convert.ToDecimal(totalDiscount),
                TotalTaxAmount = tax,
            }
        };
    }
}

public class CreateInvocieRequest
{
    public Guid ContactId { get; set; }
    public List<AddItemsToEntityRequest> InvoiceItems { get; set; }
}

public class ContactInfo
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}

public class InviceDto
{
    public Guid? Id { get; set; } = null;
    public int No { get; set; }
    public ContactInfo ContactInfo { get; set; } = null;
    public string CreatorName { get; set; }
    public decimal TotalDiscountAmount { get; set; }
    public decimal InvoiceCost { get; set; }
    public decimal InvoiceProfit { get; set; }
    public decimal TotalTaxAmount { get; set; }
    public decimal InvoiceTotalAmount { get; set; }
    public DateTime CreatedOn { get; set; }
    public decimal Total =>
        (InvoiceTotalAmount + TotalTaxAmount + InvoiceProfit) + TotalDiscountAmount - 5;
    public int ItemCount => 100;
}

public class InvoiceItem
{
    public Guid ItemId { get; set; }
    public Guid ProductId { get; set; }
}

public record AddItemsToEntityRequest
{
    public Guid ProductId { get; set; }
    public Guid ItemId { get; set; }
    public double Quantity { get; set; }
    public double SellingPrice { get; set; }
    public double Discount { get; set; }
}

public record Payment
{
    public double Amount { get; init; }
    public string Comment { get; init; }
}
#endregion


#region Delete

public class DeleteInvocieEndpoint : Endpoint<DeleteInvoiceRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/Invocie/Delete");

        Permissions();
    }

    public override async Task HandleAsync(DeleteInvoiceRequest req, CancellationToken ct)
    {
        Response = new Response() { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteInvoiceRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}

#endregion

#region Delete

public class GetInvocieEndpoint : Endpoint<GetInvoiceRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Inventory)}/Invocie/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetInvoiceRequest req, CancellationToken ct)
    {
        Response = new Response<InviceDto>()
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new()
            {
                ContactInfo = null,
                CreatedOn = DateTime.Now,
                CreatorName = "ahmed",
                Id = null,
                InvoiceCost = 0,
                InvoiceProfit = 0,
                InvoiceTotalAmount = 0,
                No = 1,
                TotalDiscountAmount = 2,
                TotalTaxAmount = 3
            }
        };
    }
}

public class GetInvoiceRequest
{
    public Guid Id { get; set; }
}

#endregion

#region Update
internal class UpdateInvoiceEndpoint : Endpoint<UpdateInvocieRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Inventory)}/Invocie/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateInvocieRequest req, CancellationToken ct)
    {
        var cost = 10;
        var taxPrec = 3;

        int tax = (taxPrec * 100) / taxPrec;

        var totalCost = req.InvoiceItems.Sum(x => x.Quantity) * cost;

        var total = totalCost * req.InvoiceItems.Sum(x => x.Quantity);

        var totalDiscount = req.InvoiceItems.Sum(x => x.Discount);

        var profit = total - totalCost - totalDiscount - totalCost + 0.2;

        Response = new Response<InviceDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new InviceDto
            {
                ContactInfo = null,
                Id = null,
                CreatedOn = DateTime.UtcNow,
                CreatorName = "ahmed",
                InvoiceProfit = Convert.ToDecimal(profit),
                InvoiceCost = Convert.ToDecimal(totalCost),
                InvoiceTotalAmount = Convert.ToDecimal(total),
                No = 100,
                TotalDiscountAmount = Convert.ToDecimal(totalDiscount),
                TotalTaxAmount = tax,
            }
        };
    }
}

public class UpdateInvocieRequest
{
    public Guid InvocieId { get; set; }
    public Guid ContactId { get; set; }
    public List<AddItemsToEntityRequest> InvoiceItems { get; set; }
}
#endregion
