using eCommerce.Domain.Abstractions.Contexts;
using eCommerce.Domain.Abstractions.Repositories;
using eCommerce.Domain.Constants;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Entities.Shared;
using eCommerce.Domain.Exceptions;
using eCommerce.Persistence.Builders;
using eCommerce.Presentation.Features.Identity.Users.Dto;
using eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Create;
using eCommerce.Presentation.Json.Service;
using eCommerce.Presentation.Jwt.Service;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Presentation.Features.Identity.Users.DaoService;

public sealed class UserDaoService : IUserDaoService
{
    readonly IeCommerceDbContext _context;
    readonly IRepository<User> _userRepository;
    readonly IRepository<Notification> _notificationRepository;
    readonly IJwtService _jwtService;
    readonly IJsonService _jsonService;
    readonly IMapper _mapper;
    readonly UserManager<User> _userManager;

    readonly string _success;

    public UserDaoService(
        IeCommerceDbContext context,
        IRepository<User> userRepository,
        IRepository<Notification> notificationRepository,
        UserManager<User> userManager,
        IJwtService jwtService,
        IJsonService jsonService
    )
    {
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
        _userRepository = userRepository;
        _userManager = userManager;
        _notificationRepository = notificationRepository;
        _context = context;
        _jwtService = jwtService;
        _jsonService = jsonService;
        _success = "Operation Done Successfully";
    }

    public async Task<Response> CreatAsync(CreateUserRequest request, CancellationToken ct)
    {
        var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var user = _mapper.Map<User>(request);

            if (user.Profile != null)
            {
                modifiedRows++;
                if (user.Profile.Address != null)
                {
                    modifiedRows++;
                }
            }

            modifiedRows++;
            user = await _userRepository.CreateAsync(user, ct);
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
            await _notificationRepository.CreateAsync(notification);
            #endregion

            var success = await _context.IsDoneAsync(modifiedRows, ct);
            if (success)
            {
                await transaction.CommitAsync(ct);
                var userDto = _mapper.Map<UserDto>(user);
                var tokenDto = await _jwtService.GenerateTokenAsync(user, ct);

                return new Response<CreateUserResponse>
                {
                    IsSuccess = true,
                    Message = _success,
                    Result = new CreateUserResponse { User = userDto, Token = tokenDto }
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
