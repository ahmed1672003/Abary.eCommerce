using eCommerce.Domain.Bases.Request;
using eCommerce.Domain.Enums.User;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;

public sealed record GetAllUsersRequest(UserOrderBy UserOrderBy = UserOrderBy.CreatedOn)
    : PaginateRequest();
