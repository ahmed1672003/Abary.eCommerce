namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;

public sealed record GetUserRequest
{
    public Guid Id { get; set; }
}
