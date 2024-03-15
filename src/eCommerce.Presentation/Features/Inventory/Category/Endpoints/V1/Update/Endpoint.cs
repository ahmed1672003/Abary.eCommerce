using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Categories.Service;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Update;

public sealed class UpdateCategoryEndpoint : Endpoint<UpdateCategoryRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Put($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Category)}/{nameof(Update)}");
        Permissions(SystemConstants.Security.Inventory.Categories.Update);
    }

    public override async Task HandleAsync(UpdateCategoryRequest req, CancellationToken ct) =>
        Response = await Resolve<ICategoryService>().UpdateAsync(req, ct);
}
