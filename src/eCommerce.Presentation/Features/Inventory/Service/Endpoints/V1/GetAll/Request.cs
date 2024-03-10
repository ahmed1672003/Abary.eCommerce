using eCommerce.Domain.Bases.Request;
using eCommerce.Domain.Enums.Inventory.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.GetAll;

public sealed record GetAllServicesRequest(ServiceOrderBy OrderBy = ServiceOrderBy.CreatedOn)
    : PaginateRequest;
