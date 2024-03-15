using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Categories.Service;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.GetAll;

public sealed class GetAllCategoriesEndpoint : Endpoint<GetAllCategoriesRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Get($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Category)}/{nameof(GetAll)}");
        Permissions(SystemConstants.Security.Inventory.Categories.GetAll);
    }

    public override async Task HandleAsync(GetAllCategoriesRequest req, CancellationToken ct) =>
        Response = await Resolve<ICategoryService>().GetAllAsync(req, ct);
}
