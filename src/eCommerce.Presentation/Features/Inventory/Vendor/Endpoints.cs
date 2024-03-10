using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace eCommerce.Presentation.Features.Inventory.Vendor;

#region Create
internal class CreateVendorEndpoint : Endpoint<CreateVendorRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Inventory)}/Vendor/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateVendorRequest req, CancellationToken ct)
    {
        Response = new Response<VendorDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new VendorDto
            {
                Id = Guid.NewGuid(),
                Address = req.Address,
                Comment = req.Comment,
                Email = req.Email,
                Name = req.Name,
                Phone = req.Phone,
                Photo = req.Photo
            }
        };
    }
}

public class VendorDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Photo { get; set; }
    public string Comment { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public string Address { get; set; }
}

public class CreateVendorRequest
{
    public string? Name { get; set; }
    public string Photo { get; set; }
    public string Comment { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public string Address { get; set; }
}

public class CreateVendorValidator : Validator<CreateVendorRequest>
{
    public CreateVendorValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Phone)
            .NotNull()
            .WithMessage("phone is required")
            .NotEmpty()
            .WithMessage("phone is required");

        RuleFor(x => x.Comment)
            .MaximumLength(3000)
            .WithMessage("max length 3000")
            .MinimumLength(1)
            .WithMessage("min length 1")
            .When(req => !string.IsNullOrEmpty(req.Comment));
    }
}
#endregion

#region Delete

public class DeleteVendorEndpoint : Endpoint<DeleteVendorRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/Vendor/Delete");
        Permissions();
    }

    public override async Task HandleAsync(DeleteVendorRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteVendorRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
#endregion

#region Update

internal class UpdateVendorEndpoint : Endpoint<UpdateVendorRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Inventory)}/Vendor/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateVendorRequest req, CancellationToken ct)
    {
        Response = new Response<VendorDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new VendorDto
            {
                Id = req.Id,
                Address = req.Address,
                Comment = req.Comment,
                Email = req.Email,
                Name = req.Name,
                Phone = req.Phone,
                Photo = req.Photo
            }
        };
    }
}

public class UpdateVendorRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Photo { get; set; }
    public string Comment { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public string Address { get; set; }
}
#endregion

#region Get


public class GetVendorEndpoint : Endpoint<GetVendorRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Inventory)}/Vendor/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetVendorRequest req, CancellationToken ct)
    {
        Response = new Response<VendorDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new VendorDto
            {
                Id = req.Id,
                Name = "mohamed",
                Address = "cairo",
                Comment = "",
                Email = "vendortest@atechnologioes.info",
                Phone = "+201938837423",
                Photo = "server/wwwroot/photos"
            }
        };
    }
}

public class GetVendorRequest
{
    public Guid Id { get; set; }
}
#endregion
