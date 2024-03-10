using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.HttpSys;

namespace eCommerce.Presentation.Features.Inventory.Deal;

#region Create
internal class CreateDealEndpoint : Endpoint<CreateDealRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Identity)}/Deal/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateDealRequest req, CancellationToken ct)
    {
        Response = new Response<DealDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new DealDto
            {
                Id = Guid.NewGuid(),
                Email = req.Email,
                DealName = req.DealName,
                Phone = req.Phone,
                Photo = req.Photo,
                Note = req.Note,
                ContactName = req.ContactName
            }
        };
    }
}

public class DealDto
{
    public Guid Id { get; set; }
    public string ContactName { get; set; }
    public string? DealName { get; set; }
    public string Photo { get; set; }
    public string Note { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
}

public class CreateDealRequest
{
    public string? DealName { get; set; }
    public string? Photo { get; set; }
    public string ContactName { get; set; }

    public string? Note { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
#endregion

#region Delete

public class DeleteDealEndpoint : Endpoint<DeleteDealRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Identity)}/Deal/Delete");
        Permissions();
    }

    public override async Task HandleAsync(DeleteDealRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteDealRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
#endregion

#region Update
internal class UpdateDealEndpoint : Endpoint<UpdateDealRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Identity)}/Deal/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateDealRequest req, CancellationToken ct)
    {
        Response = new Response<DealDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new DealDto
            {
                Id = Guid.NewGuid(),
                Email = req.Email,
                DealName = req.DealName,
                Phone = req.Phone,
                Photo = req.Photo,
                Note = req.Note,
                ContactName = req.ContactName
            }
        };
    }
}

public class UpdateDealRequest
{
    public Guid Id { get; set; }
    public string? DealName { get; set; }
    public string? Photo { get; set; }
    public string ContactName { get; set; }

    public string? Note { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
#endregion

#region Get
public class GetDealEndpoint : Endpoint<GetDealRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Identity)}/Deal/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetDealRequest req, CancellationToken ct)
    {
        Response = new Response<DealDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new DealDto
            {
                Id = Guid.NewGuid(),
                Email = "contact@atechnologies.info",
                DealName = "sell",
                Phone = "+201038222932",
                Photo = "server/photo",
                Note = "note",
                ContactName = "mohamed"
            }
        };
    }
}

public class GetDealRequest
{
    public Guid Id { get; set; }
}
#endregion
