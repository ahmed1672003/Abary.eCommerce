using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;
using eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

namespace eCommerce.Presentation.Features.Inventory.Categories.DaoService;

public sealed class CategoryDaoService : ICategoryDaoService
{
    public Task<Response> CreateAsync(CreateCategoryRequest request, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> UpdateAsync(UpdateCategoryRequest request, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> DeleteAsync(DeleteCategoryRequest request, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> GetAsync(GetCategoryRequest request, CancellationToken ct) =>
        throw new NotImplementedException();

    public Task<Response> GetAllAsync(GetAllCategoriesRequest request, CancellationToken ct) =>
        throw new NotImplementedException();
}
