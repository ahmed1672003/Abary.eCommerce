using eCommerce.Presentation.Features.Identity.Users.Dto;
using eCommerce.Presentation.Jwt.Dto;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;

public sealed record CreateUserResponse
{
    public UserDto User { get; set; }
    public TokenDto Token { get; set; }
}
