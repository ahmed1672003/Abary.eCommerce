namespace eCommerce.Domain.Bases.Request;

public record PaginateRequest(
    int Page = 1,
    int Size = 10,
    OrderByDirection OrderByDirection = OrderByDirection.Ascending,
    string Search = ""
);
