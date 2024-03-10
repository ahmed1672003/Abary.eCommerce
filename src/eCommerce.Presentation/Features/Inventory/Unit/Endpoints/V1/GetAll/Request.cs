using eCommerce.Domain.Bases.Request;
using eCommerce.Domain.Enums.Inventory.Unit;

namespace eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;

public sealed record GetAllUnitsRequest(UnitOrderBy OrderBy = UnitOrderBy.CreatedOn)
    : PaginateRequest;
