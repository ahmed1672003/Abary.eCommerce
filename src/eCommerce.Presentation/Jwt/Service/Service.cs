using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eCommerce.Domain.Abstractions.Repositories;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Persistence.Settings;
using eCommerce.Presentation.Jwt.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pavon.Persistence.Builders;

namespace eCommerce.Presentation.Jwt.Service;

public sealed class JwtService : IJwtService
{
    readonly IOptions<JwtSettings> _options;
    readonly IRepository<User> _userRepository;
    readonly IRepository<UserClaim> _userClaimRepository;
    readonly IRepository<Role> _roleRepository;
    readonly IRepository<UserRole> _userRoleRepository;
    readonly IRepository<UserPermission> _userPermissionRepository;

    readonly JwtSettings _jwtSettings;

    public JwtService(
        IOptions<JwtSettings> options,
        IRepository<User> userRepository,
        IRepository<UserClaim> userClaimRepository,
        IRepository<Role> roleRepository,
        IRepository<UserRole> userRoleRepository,
        IRepository<UserPermission> userPermissionRepository
    )
    {
        _options = options;
        _jwtSettings = options.Value;
        _userRepository = userRepository;
        _userClaimRepository = userClaimRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _userPermissionRepository = userPermissionRepository;
    }

    public async Task<TokenDto> GenerateTokenAsync(User user, CancellationToken ct)
    {
        var expire = DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpireDate);
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

        return new TokenDto { AccessToken = accessToken, Expiration = expire, };
    }

    #region Helpers
    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user, CancellationToken ct)
    {
        var claims = new List<Claim>(0);

        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new(CustomeClaimTypes.Email.ToString(), user.Email));

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new(CustomeClaimTypes.UserName.ToString(), user.UserName));

        if (!string.IsNullOrEmpty(user.Id.ToString()))
            claims.Add(new(CustomeClaimTypes.UserId.ToString(), user.Id.ToString()));

        if (!string.IsNullOrEmpty(user.PhoneNumber))
            claims.Add(new(CustomeClaimTypes.PhoneNumber.ToString(), user.PhoneNumber));

        claims.AddRange(await GenerateUserClaimsAsync(user, ct));

        return claims;
    }

    private async Task<List<Claim>> GenerateUserClaimsAsync(User user, CancellationToken ct)
    {
        var claims = new List<Claim>(0);

        var userPermissionsSpecification = new SpecificationBuilder<UserClaim>()
            .HasCriteria(x => x.UserId == user.Id)
            .Build();

        var userClaims = await _userClaimRepository.GetAllAsync(userPermissionsSpecification, ct);
        claims.AddRange(await userClaims.Select(x => x.ToClaim()).ToListAsync(ct));

        var userRolesSpecification = new SpecificationBuilder<UserRole>()
            .HasCriteria(x => x.UserId == user.Id)
            .HasIncludeString($"{nameof(UserRole.Role)}.{nameof(Role.Claims)}")
            .Build();

        var userRoles = await _userRoleRepository.GetAllAsync(userRolesSpecification, ct);

        foreach (var userRole in await userRoles.ToListAsync(ct))
            claims.AddRange(userRole.Role.Claims.Select(x => x.ToClaim()));

        var userPermissionSpecification = new SpecificationBuilder<UserPermission>()
            .HasCriteria(x => x.UserId == user.Id)
            .HasInclude(x => x.Permission)
            .HasOrderBy(x => x.Permission.Module)
            .Build();

        var userPermissions = await _userPermissionRepository.GetAllAsync(
            userPermissionSpecification,
            ct
        );

        claims.AddRange(
            userPermissions.Select(x => new Claim(
                CustomeClaimTypes.Permissions.ToString(),
                x.Permission.Value
            ))
        );

        return claims;
    }
    #endregion
}
