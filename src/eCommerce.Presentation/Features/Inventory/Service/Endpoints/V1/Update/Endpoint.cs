using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Update;
using eCommerce.Presentation.Features.Inventory.Services.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Get;

internal sealed class UpdateServiceEndpoint : Endpoint<UpdateServiceRequest, Response>
{
    public override void Configure()
    {
        Put($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Service)}/{nameof(Update)}");
        Permissions(SystemConstants.Security.Inventory.Services.Update);
    }

    public override async Task HandleAsync(UpdateServiceRequest req, CancellationToken ct) =>
        Response = await Resolve<IServiceService>().UpdateAsync(req, ct);
}
