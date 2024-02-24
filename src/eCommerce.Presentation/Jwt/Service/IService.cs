using eCommerce.Domain.Entities.Identity;
using eCommerce.Presentation.Jwt.Dto;

namespace eCommerce.Presentation.Jwt.Service;

public interface IJwtService
{
    Task<TokenDto> GenerateTokenAsync(User user, CancellationToken ct);
}
