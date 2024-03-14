using System.Linq.Expressions;
using AutoMapper;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Domain.Exceptions;
using eCommerce.Presentation.Extensions;
using eCommerce.Presentation.Features.Inventory.Services.Dto;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Services.DaoService;

public sealed class ServiceDaoService : IServiceDaoService
{
    readonly IeCommerceDbContext _context;
    readonly AutoMapper.IMapper _mapper;

    readonly DbSet<Service> _services;

    readonly string _success;

    public ServiceDaoService(IeCommerceDbContext context)
    {
        _context = context;

        _services = _context.Set<Service>();

        _success = "operation done successfully";

        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateServiceRequest, Service>();

            cfg.CreateMap<UpdateServiceRequest, Service>();

            cfg.CreateMap<Service, ServiceDto>();
        });

        _mapper = mapperCfg.CreateMapper();
    }

    public async Task<Response> CreateAsync(CreateServiceRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var service = _mapper.Map<Service>(request);

            modifiedRows++;
            var entry = await _services.AddAsync(service);
            service = entry.Entity;

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);

                var result = _mapper.Map<ServiceDto>(service);

                return new Response<ServiceDto>
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

    public async Task<Response> UpdateAsync(UpdateServiceRequest request, CancellationToken ct)
    {
        var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var service = await _services.AsNoTracking().FirstAsync(x => x.Id == request.Id);

            _mapper.Map(request, service);

            modifiedRows++;
            var entry = _services.Update(service);

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);

                var result = _mapper.Map<ServiceDto>(service);

                return new Response<ServiceDto>
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

    public async Task<Response> DeleteAsync(DeleteServiceRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var service = await _services.AsNoTracking().FirstAsync(x => x.Id == request.Id);

            modifiedRows++;
            _services.Remove(service);

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
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

    public async Task<Response> GetAsync(GetServiceRequest reqyuest, CancellationToken ct)
    {
        try
        {
            var service = await _services.AsNoTracking().FirstAsync(x => x.Id == reqyuest.Id);

            var result = _mapper.Map<ServiceDto>(service);

            return new Response<ServiceDto>
            {
                IsSuccess = true,
                Message = _success,
                Result = result
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
        }
    }

    public async Task<Response> GetAllAsync(
        GetAllServicesRequest request,
        Expression<Func<Service, object>> orderBy,
        CancellationToken ct
    )
    {
        try
        {
            var query = _services.AsNoTracking();
            var totalCount = await query.CountAsync(ct);
            query = query.Paginate(request, orderBy);

            if (request.IsDeleted)
            {
                query = query.IgnoreQueryFilters().Where(x => x.IsDeleted);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x =>
                    x.Name.ToLower().Contains(request.Search)
                    || x.Price.ToString().Contains(request.Search)
                    || x.Description.ToLower().Contains(request.Search)
                );
            }

            var result = _mapper.Map<IEnumerable<ServiceDto>>(query);

            return new PaginationResponse<IEnumerable<ServiceDto>>
            {
                IsSuccess = true,
                Message = _success,
                Count = result.Count(),
                PageNumber = request.Page,
                PageSize = request.Size,
                TotalCount = totalCount,
                Result = result
            };
        }
        catch (Exception ex)
        {
            throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
        }
    }
}
