using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using eCommerce.Domain.Enums.Identity.User;
using eCommerce.Persistence.Settings;
using eCommerce.Presentation.Jwt.Dto;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Presentation.Jwt.Service;

public sealed class JwtService : IJwtService
{
    readonly IeCommerceDbContext _context;
    readonly JwtSettings _jwtSettings;
    readonly DbSet<User> _users;
    readonly DbSet<UserClaim> _userClaims;
    readonly DbSet<Role> _roles;
    readonly DbSet<UserRole> _userRoles;
    readonly DbSet<UserPermission> _userPermissions;

    public JwtService(IeCommerceDbContext context, JwtSettings jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings;
        _users = _context.Set<User>();
        _userClaims = _context.Set<UserClaim>();
        _roles = _context.Set<Role>();
        _userRoles = _context.Set<UserRole>();
        _userPermissions = _context.Set<UserPermission>();
    }

    public async Task<TokenDto> GenerateTokenAsync(
        User user,
        LoginProvider loginProvider,
        CancellationToken ct
    )
    {
        var expire = DateTime.Now.AddSeconds(_jwtSettings.AccessTokenExpireDate);
        var symmetricSecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_jwtSettings.Secret)
        );
        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: await GetUserClaimsAsync(user, ct),
            expires: expire,
            signingCredentials: new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new TokenDto
        {
            UserId = user.Id,
            LoginProvider = loginProvider.ToString(),
            Name = nameof(AuthenticationTypeName.JWT),
            Schema = nameof(AuthSchema.Bearer),
            ExpiresIn = _jwtSettings.AccessTokenExpireDate,
            Value = accessToken,
            RefreshToken = GenerateRefreshToken(),
        };
    }

    #region Helpers
    private Task<IEnumerable<Claim>> GetUserClaimsAsync(User user, CancellationToken ct)
    {
        var claims = new List<Claim>(0);

        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new(nameof(CustomeClaimTypes.Email), user.Email));

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new(nameof(CustomeClaimTypes.UserName), user.UserName));

        if (!string.IsNullOrEmpty(user.Id.ToString()))
            claims.Add(new(nameof(CustomeClaimTypes.UserId), user.Id.ToString()));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            claims.Add(new(nameof(CustomeClaimTypes.PhoneNumber), user.PhoneNumber));

        claims.AddRange(user.Claims.Select(x => x.ToClaim()));

        foreach (var userRole in user.UserRoles)
            claims.AddRange(userRole.Role.Claims.Select(x => x.ToClaim()));

        claims.AddRange(
            user.UserPremissions.OrderBy(x => x.Permission.Value)
                .Select(x => new Claim(nameof(CustomeClaimTypes.Permissions), x.Permission.Value))
        );

        return Task.FromResult(claims.AsEnumerable());
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return string.Concat(Convert.ToBase64String(randomNumber), Guid.NewGuid().ToString());
    }
    #endregion
}
