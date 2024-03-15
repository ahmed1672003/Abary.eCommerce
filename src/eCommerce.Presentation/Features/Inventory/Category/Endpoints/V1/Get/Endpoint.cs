using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Categories.Service;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Get;

public sealed class GetCategoryEndpoint : Endpoint<GetCategoryRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Category)}/{nameof(Get)}");
        Permissions(SystemConstants.Security.Inventory.Categories.Get);
    }

    public override async Task HandleAsync(GetCategoryRequest req, CancellationToken ct) =>
        Response = await Resolve<ICategoryService>().GetAsync(req, ct);
}
