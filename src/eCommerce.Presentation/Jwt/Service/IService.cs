using eCommerce.Presentation.Jwt.Dto;

namespace eCommerce.Presentation.Jwt.Service;

public interface IJwtService
{
    Task<TokenDto> GenerateTokenAsync(User user, LoginProvider loginProvider, CancellationToken ct);
}
