using IsolationLevel = System.Data.IsolationLevel;

namespace eCommerce.Presentation.Features.Identity.Users.DaoService;

public sealed class UserDaoService : IUserDaoService
{
    readonly IeCommerceDbContext _context;
    readonly IJwtService _jwtService;
    readonly IJsonService _jsonService;
    readonly AutoMapper.IMapper _mapper;
    readonly IHttpContextAccessor _httpContextAccessor;
    readonly UserManager<User> _userManager;
    readonly SignInManager<User> _signInManager;

    readonly DbSet<User> _users;
    readonly DbSet<Notification> _notifications;
    readonly DbSet<UserToken> _userTokens;
    readonly DbSet<UserLogin> _userLogins;

    readonly string _success;
    readonly string _userId;

    public UserDaoService(
        IeCommerceDbContext context,
        IJwtService jwtService,
        IJsonService jsonService,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor,
        SignInManager<User> signInManager
    )
    {
        _context = context;

        _userManager = userManager;
        _jwtService = jwtService;
        _jsonService = jsonService;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;

        _users = _context.Set<User>();
        _notifications = _context.Set<Notification>();
        _userTokens = _context.Set<UserToken>();
        _userLogins = _context.Set<UserLogin>();

        _userId = _httpContextAccessor.HttpContext.User.FindFirstValue(
            nameof(CustomeClaimTypes.UserId)
        );

        _success = "operation done successfully";
        #region Initial Mapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserRequest, User>();
            cfg.CreateMap<CreateUserRequest.CreatUserProfile, UserProfile>();
            cfg.CreateMap<CreateUserRequest.CreatUserProfile.CreateAddressRequest, Address>();

            cfg.CreateMap<UpdateUserRequest, User>();
            cfg.CreateMap<UpdateUserRequest.UpdateUserProfile, UserProfile>();
            cfg.CreateMap<UpdateUserRequest.UpdateUserProfile.UpdateAddressRequest, Address>();

            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<UserProfile, UserDto.UserProfileDto>();
            cfg.CreateMap<Address, UserDto.UserProfileDto.AddressDto>();

            cfg.CreateMap<UserPermission, UserDto.UserPermissionDto>();
            cfg.CreateMap<Permission, UserDto.UserPermissionDto.PermissionDto>();

            cfg.CreateMap<RegisterUserRequest, User>()
                .ForMember(
                    dist => dist.NormalizedEmail,
                    cfg => cfg.MapFrom(src => src.Email.ToUpper())
                )
                .ForMember(
                    dist => dist.NormalizedUserName,
                    cfg => cfg.MapFrom(src => src.UserName.ToUpper())
                );

            cfg.CreateMap<TokenDto, UserToken>().ReverseMap();
        });
        _mapper = mapperConfig.CreateMapper();
        #endregion
    }

    public async Task<Response> RegisterAsync(RegisterUserRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var user = _mapper.Map<User>(request);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

            var tokenDto = await _jwtService.GenerateTokenAsync(user, request.AuthProvider, ct);

            var token = _mapper.Map<UserToken>(tokenDto);
            user.Token = token;

            modifiedRows++;
            await _users.AddAsync(user);

            var jsonModel = await _jsonService.SeralizeAsync(user, ct);

            #region Notify System
            var notification = new NotificationBuilder()
                .WithCommandType(CommandType.Register)
                .WithEntity(EntityName.User)
                .WithFeature(FeatureName.User)
                .WithModule(ModuleName.Identity)
                .WithServiceName(nameof(UserDaoService))
                .WithEntityValue(jsonModel)
                .WithEntityId(user.Id)
                .WithCreatedBy(Guid.Parse(SystemConstants.SYSTEM_KEY))
                .Build();

            modifiedRows++;
            await _notifications.AddAsync(notification, ct);
            #endregion

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);

                return new Response<TokenDto>
                {
                    IsSuccess = true,
                    Message = _success,
                    Result = tokenDto
                };
            }

            await transaction.RollbackAsync(ct);
            throw new DatabaseTransactionException();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(ct);
            throw new DatabaseTransactionException(ex.Message, ex.InnerException);
        }
    }

    public async Task<Response> AuthenticateAsync(
        AuthenticateUserRequest request,
        CancellationToken ct
    )
    {
        var tokenDto =
            request.RefreshToken != null
                ? await RefreshTokenAsync(request, ct)
                : await LoginAsync(request, ct);

        return new Response<TokenDto>
        {
            IsSuccess = true,
            Message = _success,
            Result = tokenDto,
        };
    }

    public async Task<Response> LogoutAsync(CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;
                var token = await _userTokens.FirstAsync(x => x.UserId == Guid.Parse(_userId));

                modifiedRows += await _userTokens
                    .AsNoTracking()
                    .Where(x => x.UserId == Guid.Parse(_userId))
                    .ExecuteDeleteAsync(ct);

                if (modifiedRows == 1)
                {
                    await transaction.CommitAsync(ct);
                    return new Response { IsSuccess = true, Message = _success, };
                }
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message, ex.InnerException);
            }
        }
    }

    public async Task<Response> ChangePasswordAsync(
        ChangePasswordRequest request,
        CancellationToken ct
    )
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;

                var user = await _userManager
                    .Users.Include(x => x.Token)
                    .FirstAsync(x => x.Id == Guid.Parse(_userId));

                var tokenDto = await _jwtService.GenerateTokenAsync(user, LoginProvider.System, ct);

                var token = _mapper.Map<UserToken>(tokenDto);

                if (user.Token != null)
                    modifiedRows += await _userTokens
                        .AsNoTracking()
                        .Where(x => x.UserId == user.Id)
                        .ExecuteDeleteAsync(ct);

                var result = await _userManager.ChangePasswordAsync(
                    user,
                    request.OldPassword,
                    request.NewPassword
                );

                if (modifiedRows == 1 && result.Succeeded)
                {
                    await transaction.CommitAsync(ct);

                    return new Response { IsSuccess = true, Message = _success, };
                }

                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message, ex);
            }
        }
    }

    public async Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(ct))
        {
            try
            {
                var modifiedRows = 0;

                var user = _mapper.Map<User>(request);

                var jsonModel = await _jsonService.SeralizeAsync(user, ct);

                var result = await _userManager.CreateAsync(user, request.Password);

                #region Notify System
                var notification = new NotificationBuilder()
                    .WithCommandType(CommandType.Create)
                    .WithEntity(EntityName.User)
                    .WithFeature(FeatureName.User)
                    .WithModule(ModuleName.Identity)
                    .WithServiceName(nameof(UserDaoService))
                    .WithEntityValue(jsonModel)
                    .WithEntityId(user.Id)
                    .WithCreatedBy(Guid.Parse(SystemConstants.SYSTEM_KEY))
                    .Build();

                modifiedRows++;
                await _notifications.AddAsync(notification, ct);
                #endregion

                var success = await _context.IsDoneAsync(modifiedRows, ct);
                if (success && result.Succeeded)
                {
                    await transaction.CommitAsync(ct);
                    var userDto = _mapper.Map<UserDto>(user);

                    return new Response<UserDto>
                    {
                        IsSuccess = true,
                        Message = _success,
                        Result = userDto
                    };
                }
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message);
            }
        }
    }

    public async Task<Response> UpdateAsync(UpdateUserRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;

                var user = await _users
                    .AsNoTracking()
                    .Include(x => x.Profile)
                    .ThenInclude(x => x.Address)
                    .FirstAsync(x => x.Id == request.UserId);

                if (user.Profile != null)
                {
                    modifiedRows++;
                    if (user.Profile.Address != null)
                    {
                        modifiedRows++;
                    }
                }

                _mapper.Map(request, user);

                modifiedRows++;
                _users.Update(user);

                var success = await _context.IsDoneAsync(modifiedRows, ct);

                if (success)
                {
                    await transaction.CommitAsync(ct);

                    var result = _mapper.Map<UserDto>(user);

                    return new Response<UserDto>
                    {
                        IsSuccess = true,
                        Message = _success,
                        Result = result
                    };
                }

                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);

                throw new DatabaseTransactionException(ex.Message, ex.InnerException);
            }
        }
    }

    public async Task<Response> GetAsync(GetUserRequest request, CancellationToken ct)
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var user = await _users
                    .AsNoTracking()
                    .Include(x => x.Profile)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.UserPremissions)
                    .ThenInclude(x => x.Permission)
                    .FirstAsync(x => x.Id == request.Id);

                var userDto = _mapper.Map<UserDto>(user);

                return new Response<UserDto>
                {
                    IsSuccess = true,
                    Message = _success,
                    Result = userDto,
                };
            }
            catch (Exception ex)
            {
                throw new DatabaseExecuteQueryException(ex.Message, ex);
            }
        }
    }

    public async Task<Response> GetAllAsync(
        GetAllUsersRequest request,
        Expression<Func<User, object>> orderBy,
        CancellationToken ct
    )
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var query = _users
                    .AsNoTracking()
                    .Include(x => x.Profile)
                    .ThenInclude(x => x.Address)
                    .Include(x => x.UserPremissions)
                    .ThenInclude(x => x.Permission)
                    .AsQueryable();

                var totalCount = await query.CountAsync(ct);

                query = query.Paginate(request, orderBy);

                if (request.IsDeleted)
                {
                    query = query.IgnoreQueryFilters().Where(x => x.IsDeleted);
                    totalCount = await query.CountAsync(ct);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    query = query.Where(x =>
                        x.UserName.Contains(request.Search.ToLower())
                        || x.Email.Contains(request.Search.ToLower())
                        || (
                            x.Profile != null ? x.Profile.FirstName.Contains(request.Search) : false
                        )
                        || (x.Profile != null ? x.Profile.LastName.Contains(request.Search) : false)
                    );
                }

                var userDto = _mapper.Map<IEnumerable<UserDto>>(query);

                return new PaginationResponse<IEnumerable<UserDto>>
                {
                    Count = userDto.Count(),
                    IsSuccess = true,
                    Message = _success,
                    PageNumber = request.Page,
                    PageSize = request.Size,
                    Result = userDto,
                    TotalCount = totalCount,
                };
            }
            catch (Exception ex)
            {
                throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
            }
        }
    }

    #region Helpers

    async Task<TokenDto> LoginAsync(AuthenticateUserRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;

                var user = await _users
                    .AsNoTracking()
                    .Include(x => x.Claims)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Claims)
                    .Include(x => x.UserPremissions)
                    .ThenInclude(x => x.Permission)
                    .Include(x => x.Token)
                    .FirstAsync(
                        x =>
                            new EmailAddressAttribute().IsValid(request.EmailOrUserName)
                                ? x.NormalizedEmail == request.EmailOrUserName.ToUpper()
                                : x.NormalizedUserName == request.EmailOrUserName.ToUpper(),
                        ct
                    );

                var tokenDto = await _jwtService.GenerateTokenAsync(
                    user,
                    request.LoginProvider.Value,
                    ct
                );
                var token = _mapper.Map<UserToken>(tokenDto);

                if (user.Token != null)
                    await _userTokens.Where(x => x.Id == user.Token.Id).ExecuteDeleteAsync(ct);

                modifiedRows++;
                await _userTokens.AddAsync(token);

                var success = await _context.IsDoneAsync(modifiedRows, ct);

                if (success)
                {
                    await transaction.CommitAsync(ct);

                    return tokenDto;
                }

                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message, ex.InnerException);
            }
        }
    }

    async Task<TokenDto> RefreshTokenAsync(AuthenticateUserRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;

                var user = await _users
                    .AsNoTracking()
                    .Include(x => x.Claims)
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.Claims)
                    .Include(x => x.UserPremissions)
                    .ThenInclude(x => x.Permission)
                    .Include(x => x.Token)
                    .FirstAsync(x => x.Token.RefreshToken == request.RefreshToken, ct);

                var tokenDto = await _jwtService.GenerateTokenAsync(user, LoginProvider.System, ct);
                var token = _mapper.Map<UserToken>(tokenDto);

                await _userTokens
                    .AsNoTracking()
                    .Where(x => x.UserId == user.Id && x.RefreshToken == request.RefreshToken)
                    .ExecuteDeleteAsync(ct);

                modifiedRows++;
                await _userTokens.AddAsync(token);

                var success = await _context.IsDoneAsync(modifiedRows, ct);

                if (success)
                {
                    await transaction.CommitAsync(ct);

                    return tokenDto;
                }

                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message, ex.InnerException);
            }
        }
    }

    #endregion
}
