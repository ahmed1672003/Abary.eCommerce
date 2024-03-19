using System.Data;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Units.Dto;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Units.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Units.DaoService;

public sealed class UnitDaoService : IUnitDaoService
{
    readonly IeCommerceDbContext _context;
    readonly AutoMapper.IMapper _mapper;

    readonly DbSet<Unit> _units;

    string _success = "operation done successfully";

    public UnitDaoService(IeCommerceDbContext context)
    {
        _context = context;
        _units = _context.Set<Unit>();

        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUnitRequest, Unit>();
            cfg.CreateMap<UpdateUnitRequest, Unit>();
            cfg.CreateMap<Unit, UnitDto>();
        });
        _mapper = mapperCfg.CreateMapper();
    }

    public async Task<Response> CreateAsync(CreateUnitRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(ct))
        {
            try
            {
                var modifiedRows = 0;
                var unit = _mapper.Map<Unit>(request);

                modifiedRows++;
                var entry = await _units.AddAsync(unit);

                unit = entry.Entity;

                var success = await _context.IsDoneAsync(modifiedRows, ct);

                if (success)
                {
                    await transaction.CommitAsync(ct);

                    var result = _mapper.Map<UnitDto>(unit);

                    return new Response<UnitDto>
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

    public async Task<Response> UpdateAsync(UpdateUnitRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;
                var unit = await _units.AsNoTracking().FirstAsync(x => x.Id == request.Id);

                _mapper.Map(request, unit);

                modifiedRows++;
                _units.Update(unit);

                var success = await _context.IsDoneAsync(modifiedRows, ct);

                if (success)
                {
                    await transaction.CommitAsync(ct);

                    var result = _mapper.Map<UnitDto>(unit);

                    return new Response<UnitDto>
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

    public async Task<Response> DeleteAsync(DeleteUnitRequest request, CancellationToken ct)
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var modifiedRows = 0;

                var unit = await _units.FirstAsync(x => x.Id == request.Id);

                modifiedRows++;
                _units.Remove(unit);

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
    }

    public async Task<Response> GetAsync(GetUnitRequest request, CancellationToken ct)
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var unit = await _units.AsNoTracking().FirstAsync(x => x.Id == request.Id);

                var result = _mapper.Map<UnitDto>(unit);
                await transaction.CommitAsync(ct);
                return new Response<UnitDto>
                {
                    IsSuccess = true,
                    Message = _success,
                    Result = result
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
            }
        }
    }

    public async Task<Response> GetAllAsync(
        GetAllUnitsRequest request,
        Expression<Func<Unit, object>> orderBy,
        CancellationToken ct
    )
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var query = _units.AsNoTracking();

                var totalCount = await query.CountAsync(ct);

                query = query.Paginate(request, orderBy);
                if (request.IsDeleted)
                {
                    query = query.IgnoreQueryFilters().Where(x => x.IsDeleted);
                    totalCount = await query.CountAsync(ct);
                }

                if (!string.IsNullOrEmpty(request.Search))
                    query = query.Where(x => x.Name.ToLower().Contains(request.Search.ToLower()));

                var result = _mapper.Map<IEnumerable<UnitDto>>(query);

                await transaction.CommitAsync(ct);
                return new PaginationResponse<IEnumerable<UnitDto>>
                {
                    IsSuccess = true,
                    Message = _success,
                    Result = result,
                    Count = result.Count(),
                    PageNumber = request.Page,
                    PageSize = request.Size,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
            }
        }
    }
}
