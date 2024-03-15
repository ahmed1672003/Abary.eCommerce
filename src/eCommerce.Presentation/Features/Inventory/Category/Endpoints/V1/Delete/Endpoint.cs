using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Categories.Service;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Delete;

public sealed class DeleteCategoryEndpoint : Endpoint<DeleteCategoryRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Delete($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Category)}/{nameof(Delete)}");
        Permissions(SystemConstants.Security.Inventory.Categories.Delete);
    }

    public override async Task HandleAsync(DeleteCategoryRequest req, CancellationToken ct) =>
        Response = await Resolve<ICategoryService>().DeleteAsync(req, ct);
}
