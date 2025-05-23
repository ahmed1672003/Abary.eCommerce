﻿using System.Linq.Expressions;
using eCommerce.Domain.Enums.Identity.User;
using eCommerce.Presentation.Features.Identity.Users.DaoService;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.ChangePassword;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Register;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Identity.Users.Service;

public sealed class UserService : IUserService
{
    readonly IUserDaoService _userDaoService;

    public UserService(IUserDaoService userDaoService) => _userDaoService = userDaoService;

    public Task<Response> RegisterAsync(RegisterUserRequest request, CancellationToken ct) =>
        _userDaoService.RegisterAsync(request, ct);

    public Task<Response> AuthenticateAsync(AuthenticateUserRequest reqest, CancellationToken ct) =>
        _userDaoService.AuthenticateAsync(reqest, ct);

    public Task<Response> LogoutAsync(CancellationToken ct) => _userDaoService.LogoutAsync(ct);

    public Task<Response> ChangePasswordAsync(
        ChangePasswordRequest request,
        CancellationToken ct
    ) => _userDaoService.ChangePasswordAsync(request, ct);

    public Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct) =>
        _userDaoService.CreatAsync(request, ct);

    public Task<Response> GetAsync(GetUserRequest request, CancellationToken ct) =>
        _userDaoService.GetAsync(request, ct);

    public Task<Response> GetAllAsync(GetAllUsersRequest request, CancellationToken ct)
    {
        Expression<Func<User, object>> orderBy = request.UserOrderBy switch
        {
            UserOrderBy.Id => user => user.Id,
            UserOrderBy.UserName => user => user.UserName,
            UserOrderBy.Email => user => user.Email,
            UserOrderBy.Name => user => user.Profile.FirstName,
            _ => user => user.CreatedOn,
        };
        return _userDaoService.GetAllAsync(request, orderBy, ct);
    }

    public Task<Response> UpdateAsync(UpdateUserRequest request, CancellationToken ct) =>
        _userDaoService.UpdateAsync(request, ct);
}
