using System.Linq.Expressions;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Domain.Enums.Inventory.Categories;
using eCommerce.Presentation.Features.Inventory.Categories.DaoService;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Categories.Service;

public sealed class CategoryService : ICategoryService
{
    readonly ICategoryDaoService _categoryDaoService;

    public CategoryService(ICategoryDaoService categoryDaoService) =>
        _categoryDaoService = categoryDaoService;

    public Task<Response> CreateAsync(CreateCategoryRequest request, CancellationToken ct) =>
        _categoryDaoService.CreateAsync(request, ct);

    public Task<Response> UpdateAsync(UpdateCategoryRequest request, CancellationToken ct) =>
        _categoryDaoService.UpdateAsync(request, ct);

    public Task<Response> DeleteAsync(DeleteCategoryRequest request, CancellationToken ct) =>
        _categoryDaoService.DeleteAsync(request, ct);

    public Task<Response> GetAsync(GetCategoryRequest request, CancellationToken ct) =>
        _categoryDaoService.GetAsync(request, ct);

    public Task<Response> GetAllAsync(GetAllCategoriesRequest request, CancellationToken ct)
    {
        Expression<Func<Category, object>> orderBy = request.OrderBy switch
        {
            CategoryOrderBy.Id => x => x.Id,
            CategoryOrderBy.Name => x => x.Name,
            CategoryOrderBy.CreatedOn => x => x.CreatedOn,
            _ => (Category x) => x.CreatedOn
        };
        return _categoryDaoService.GetAllAsync(request, orderBy, ct);
    }
}
