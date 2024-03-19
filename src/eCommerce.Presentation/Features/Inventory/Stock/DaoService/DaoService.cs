using System.Data;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Stocks.Dto;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Stocks.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Stocks.DaoService;

public sealed class StockDaoService : IStockDaoService
{
    readonly IeCommerceDbContext _context;
    readonly AutoMapper.IMapper _mapper;
    readonly DbSet<Stock> _stocks;
    readonly DbSet<Address> _addresses;

    readonly string _success;
    readonly string _fail;

    public StockDaoService(IeCommerceDbContext context)
    {
        _context = context;
        _stocks = _context.Set<Stock>();
        _addresses = _context.Set<Address>();

        _success = "تمت العملية بنجاح";
        _fail = "فشلت العملية";

        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateStockRequest, Stock>();
            cfg.CreateMap<CreateStockRequest.CreateAddressRequest, Address>();

            cfg.CreateMap<UpdateStockRequest, Stock>();
            cfg.CreateMap<UpdateStockRequest.UpdateAddressRequest, Address>();

            cfg.CreateMap<Stock, StockDto>();
            cfg.CreateMap<Address, StockDto.AddressDto>();
        });
        _mapper = mapperCfg.CreateMapper();
    }

    public async Task<Response> CreateAsync(CreateStockRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(ct))
        {
            try
            {
                var modifiedRows = 0;

                var stock = _mapper.Map<Stock>(request);

                if (stock is not { Address: null })
                {
                    modifiedRows++;
                }

                modifiedRows++;
                var entry = await _stocks.AddAsync(stock, ct);
                var success = await _context.IsDoneAsync(modifiedRows, ct);
                if (success)
                {
                    await transaction.CommitAsync(ct);
                    var result = _mapper.Map<StockDto>(entry.Entity);

                    return new Response<StockDto>
                    {
                        IsSuccess = true,
                        Message = _success,
                        Result = result
                    };
                }

                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(_fail);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                throw new DatabaseTransactionException(ex.Message, ex.InnerException);
            }
        }
    }

    public async Task<Response> UpdateAsync(UpdateStockRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;
                var stock = await _stocks
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .FirstAsync(x => x.Id.Equals(request.Id));

                _mapper.Map(request, stock);

                modifiedRows++;
                modifiedRows++;
                var entry = _stocks.Update(stock);

                var success = await _context.IsDoneAsync(modifiedRows, ct);
                if (success)
                {
                    await transaction.CommitAsync(ct);
                    var result = _mapper.Map<StockDto>(entry.Entity);
                    return new Response<StockDto>
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

    public async Task<Response> DeleteAsync(DeleteStockRequest request, CancellationToken ct)
    {
        using (var transaction = await _context.BeginTransactionAsync(IsolationLevel.Snapshot, ct))
        {
            try
            {
                var modifiedRows = 0;
                var stock = await _stocks
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .FirstAsync(x => x.Id.Equals(request.Id));

                if (stock.Address != null)
                {
                    modifiedRows++;
                    _addresses.Remove(stock.Address);
                }

                modifiedRows++;
                _stocks.Remove(stock);

                var success = await _context.IsDoneAsync(modifiedRows, ct);
                if (success)
                {
                    await transaction.CommitAsync(ct);
                    return new Response { IsSuccess = true, Message = _success };
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

    public async Task<Response> GetAsync(GetStockRequest request, CancellationToken ct)
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var stock = await _stocks
                    .AsNoTracking()
                    .Include(x => x.Address)
                    .FirstAsync(x => x.Id.Equals(request.Id));

                var result = _mapper.Map<StockDto>(stock);

                await transaction.CommitAsync(ct);
                return new Response<StockDto>
                {
                    IsSuccess = false,
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
        GetAllStocksRequest request,
        Expression<Func<Stock, object>> orderBy,
        CancellationToken ct
    )
    {
        using (
            var transaction = await _context.BeginTransactionAsync(IsolationLevel.ReadCommitted, ct)
        )
        {
            try
            {
                var query = _stocks.AsNoTracking();
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
                        x.Name.ToLower().Contains(request.Search)
                        || x.CreatedOn.ToString().Contains(request.Search)
                    );
                }

                var result = _mapper.Map<IEnumerable<StockDto>>(query);

                await transaction.CommitAsync(ct);
                return new PaginationResponse<IEnumerable<StockDto>>
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
                await transaction.RollbackAsync(ct);
                throw new DatabaseExecuteQueryException(ex.Message, ex.InnerException);
            }
        }
    }
}
