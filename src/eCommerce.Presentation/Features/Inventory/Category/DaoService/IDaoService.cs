using System.Linq.Expressions;
using eCommerce.Domain.Entities.Inventory;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Categories.DaoService;

public interface ICategoryDaoService
{
    Task<Response> CreateAsync(CreateCategoryRequest request, CancellationToken ct);
    Task<Response> UpdateAsync(UpdateCategoryRequest request, CancellationToken ct);
    Task<Response> DeleteAsync(DeleteCategoryRequest request, CancellationToken ct);
    Task<Response> GetAsync(GetCategoryRequest request, CancellationToken ct);
    Task<Response> GetAllAsync(
        GetAllCategoriesRequest request,
        Expression<Func<Category, object>> orderBy,
        CancellationToken ct
    );
}
