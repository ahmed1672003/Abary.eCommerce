using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Services.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Create;

internal sealed class CreateServiceEndpoint : Endpoint<CreateServiceRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Post($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Service)}/{nameof(Create)}");
        Permissions(SystemConstants.Security.Inventory.Services.Create);
    }

    public override async Task HandleAsync(CreateServiceRequest req, CancellationToken ct) =>
        Response = await Resolve<IServiceService>().CreateAsync(req, ct);
}
