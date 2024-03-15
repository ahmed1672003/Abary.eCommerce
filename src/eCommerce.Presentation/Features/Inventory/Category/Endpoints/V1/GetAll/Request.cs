using eCommerce.Domain.Bases.Request;
using eCommerce.Domain.Enums.Inventory.Categories;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;

public sealed record GetAllCategoriesRequest(CategoryOrderBy OrderBy = CategoryOrderBy.CreatedOn)
    : PaginateRequest { }
