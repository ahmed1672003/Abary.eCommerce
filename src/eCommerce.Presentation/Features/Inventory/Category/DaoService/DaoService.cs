using System.Linq.Expressions;
using AutoMapper;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Domain.Exceptions;
using eCommerce.Presentation.Extensions;
using eCommerce.Presentation.Features.Inventory.Categories.Dto;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Categories.DaoService;

public sealed class CategoryDaoService : ICategoryDaoService
{
    readonly IeCommerceDbContext _context;
    readonly AutoMapper.IMapper _mapper;
    readonly DbSet<Category> _categories;

    readonly string _success;

    public CategoryDaoService(IeCommerceDbContext context)
    {
        _context = context;
        _categories = _context.Set<Category>();

        _success = "تمت العملية بنجاح";
        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateCategoryRequest, Category>();
            cfg.CreateMap<UpdateCategoryRequest, Category>();

            cfg.CreateMap<Category, CategoryDto>();
        });

        _mapper = mapperCfg.CreateMapper();
    }

    public async Task<Response> CreateAsync(CreateCategoryRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;

            var category = _mapper.Map<Category>(request);

            modifiedRows++;
            var entry = await _categories.AddAsync(category, ct);

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);
                var result = _mapper.Map<CategoryDto>(entry.Entity);
                return new Response<CategoryDto>
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

    public async Task<Response> UpdateAsync(UpdateCategoryRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;
            var category = await _categories
                .AsNoTracking()
                .FirstAsync(x => x.Id.Equals(request.Id));
            _mapper.Map(request, category);

            modifiedRows++;
            var entry = _categories.Update(category);

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);

                var result = _mapper.Map<CategoryDto>(entry.Entity);

                return new Response<CategoryDto>
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

    public async Task<Response> DeleteAsync(DeleteCategoryRequest request, CancellationToken ct)
    {
        using var transaction = await _context.BeginTransactionAsync(ct);
        try
        {
            var modifiedRows = 0;
            var category = await _categories
                .AsNoTracking()
                .FirstAsync(x => x.Id.Equals(request.Id));

            modifiedRows++;
            _categories.Remove(category);

            var success = await _context.IsDoneAsync(modifiedRows, ct);

            if (success)
            {
                await transaction.CommitAsync(ct);
                return new Response { IsSuccess = false, Message = _success, };
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

    public async Task<Response> GetAsync(GetCategoryRequest request, CancellationToken ct)
    {
        try
        {
            var category = await _categories
                .AsNoTracking()
                .FirstAsync(x => x.Id.Equals(request.Id));
            var result = _mapper.Map<CategoryDto>(category);

            return new Response<CategoryDto>
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
        GetAllCategoriesRequest request,
        Expression<Func<Category, object>> orderBy,
        CancellationToken ct
    )
    {
        try
        {
            var query = _categories.AsNoTracking();
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

            var result = _mapper.Map<IEnumerable<CategoryDto>>(query);

            return new PaginationResponse<IEnumerable<CategoryDto>>
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
