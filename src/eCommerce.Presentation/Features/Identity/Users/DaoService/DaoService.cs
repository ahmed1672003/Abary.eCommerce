using System.ComponentModel.DataAnnotations;
using eCommerce.Domain.Abstractions.Contexts;
using eCommerce.Domain.Constants;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Entities.Shared;
using eCommerce.Domain.Exceptions;
using eCommerce.Persistence.Builders;
using eCommerce.Presentation.Features.Identity.Users.Dto;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Login;
using eCommerce.Presentation.Json.Service;
using eCommerce.Presentation.Jwt.Dto;
using eCommerce.Presentation.Jwt.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Presentation.Features.Identity.Users.DaoService;

public sealed class UserDaoService : IUserDaoService
{
    readonly IeCommerceDbContext _context;
    readonly IJwtService _jwtService;
    readonly IJsonService _jsonService;
    readonly IMapper _mapper;
    readonly UserManager<User> _userManager;
    readonly DbSet<User> _users;
    readonly DbSet<Notification> _notifications;

    readonly string _success;

    public UserDaoService(
        IeCommerceDbContext context,
        IJwtService jwtService,
        IJsonService jsonService,
        UserManager<User> userManager
    )
    {
        _userManager = userManager;
        _context = context;
        _jwtService = jwtService;
        _jsonService = jsonService;

        _users = _context.Set<User>();
        _notifications = _context.Set<Notification>();

        _success = "Operation Done Successfully";
        #region Initial Mapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserRequest, User>();
            cfg.CreateMap<CreateUserRequest.CreatUserProfile, UserProfile>();
            cfg.CreateMap<CreateUserRequest.CreatUserProfile.CreateAddressRequest, Address>();

            cfg.CreateMap<User, UserDto>();
            cfg.CreateMap<UserProfile, UserDto.UserProfileDto>();
            cfg.CreateMap<Address, UserDto.UserProfileDto.AddressDto>();
        });
        _mapper = mapperConfig.CreateMapper();
        #endregion
    }

    public async Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct)
    {
        var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var user = _mapper.Map<User>(request);

            var jsonModel = await _jsonService.SeralizeAsync(user, ct);

            await _userManager.CreateAsync(user, request.Password);

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
            if (success)
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

    public async Task<Response> LoginAsync(LoginUserRequest request, CancellationToken ct)
    {
        var user = await _users
            .AsNoTracking()
            .Include(x => x.Claims)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ThenInclude(x => x.Claims)
            .Include(x => x.UserPremissions)
            .ThenInclude(x => x.Permission)
            .FirstAsync(
                x =>
                    new EmailAddressAttribute().IsValid(request.EmailOrUserName)
                        ? x.Email == request.EmailOrUserName
                        : x.UserName == request.EmailOrUserName,
                ct
            );

        var token = await _jwtService.GenerateTokenAsync(user, ct);

        return new Response<TokenDto>()
        {
            IsSuccess = true,
            Message = _success,
            Result = token,
        };
    }

    public async Task<Response> GetAsync(GetUserRequest request, CancellationToken ct)
    {
        var user = await _users
            .AsNoTracking()
            .Include(x => x.Profile)
            .ThenInclude(x => x.Address)
            .FirstAsync(x => x.Id == request.Id);

        var userDto = _mapper.Map<UserDto>(user);

        return new Response<UserDto>
        {
            IsSuccess = true,
            Message = _success,
            Result = userDto,
        };
    }
}
