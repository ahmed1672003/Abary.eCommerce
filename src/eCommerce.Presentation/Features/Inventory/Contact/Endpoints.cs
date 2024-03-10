using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Presentation.Features.Inventory.Contact;

#region Create
internal class CreateContactEndpoint : Endpoint<CreateContactRequest, Response>
{
    public override void Configure()
    {
        Post($"{nameof(ModuleName.Identity)}/Contact/Create");
        Permissions();
    }

    public override async Task HandleAsync(CreateContactRequest req, CancellationToken ct)
    {
        Response = new Response<ContactDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new ContactDto
            {
                Id = Guid.NewGuid(),
                Email = req.Email,
                Name = req.Name,
                Phone = req.Phone,
                Photo = req.Photo,
                Note = req.Note,
            }
        };
    }
}

public class ContactDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Photo { get; set; }
    public string Note { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}

public class CreateContactRequest
{
    [Required(ErrorMessage = "contact name is required")]
    public string? Name { get; set; }
    public string? Photo { get; set; }

    [Length(1, 3000, ErrorMessage = "max length 3000")]
    public string Note { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }
}
#endregion

#region Delete

public class DeleteContactEndpoint : Endpoint<DeleteContactRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/Contact/Delete");
        Permissions();
    }

    public override async Task HandleAsync(DeleteContactRequest req, CancellationToken ct)
    {
        Response = new Response { IsSuccess = true, Message = "operation done successfully" };
    }
}

public class DeleteContactRequest
{
    [FromHeader]
    public Guid Id { get; set; }
}
#endregion

#region Update
internal class UpdateContactEndpoint : Endpoint<UpdateContactRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Identity)}/Contact/Update");
        Permissions();
    }

    public override async Task HandleAsync(UpdateContactRequest req, CancellationToken ct)
    {
        Response = new Response<ContactDto>()
        {
            IsSuccess = true,
            Message = "operation don successfully",
            Result = new ContactDto
            {
                Id = req.Id,
                Email = req.Email,
                Name = req.Name,
                Phone = req.Phone,
                Photo = req.Photo,
                Note = req.Note,
            }
        };
    }
}

public class UpdateContactRequest
{
    [Required(ErrorMessage = "contact is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "contact name is required")]
    public string? Name { get; set; }
    public string? Photo { get; set; }

    [Length(1, 3000, ErrorMessage = "max length 3000")]
    public string Note { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }
}
#endregion

#region Get
public class GetContactEndpoint : Endpoint<GetContactRequest, Response>
{
    public override void Configure()
    {
        Get($"{nameof(ModuleName.Identity)}/Contact/Get");
        Permissions();
    }

    public override async Task HandleAsync(GetContactRequest req, CancellationToken ct)
    {
        Response = new Response<ContactDto>
        {
            IsSuccess = true,
            Message = "operation done successfully",
            Result = new ContactDto
            {
                Id = req.Id,
                Name = "mohamed",
                Email = "vendortest@atechnologioes.info",
                Phone = "+201938837423",
                Photo = "server/wwwroot/photos",
                Note = "note",
            }
        };
    }
}

public class GetContactRequest
{
    public Guid Id { get; set; }
}
#endregion
