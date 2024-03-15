using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Categories.Service;

namespace eCommerce.Presentation.Features.Inventory.Categories.Endpoints.V1.Create;

public sealed class CreateCategoryEndpoint : Endpoint<CreateCategoryRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Post($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Category)}/{nameof(Create)}");
        Permissions(SystemConstants.Security.Inventory.Categories.Create);
    }

    public override async Task HandleAsync(CreateCategoryRequest req, CancellationToken ct) =>
        Response = await Resolve<ICategoryService>().CreateAsync(req, ct);
}
