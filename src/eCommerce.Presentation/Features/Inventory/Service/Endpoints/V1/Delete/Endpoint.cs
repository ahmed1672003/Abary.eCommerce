using eCommerce.Domain.Constants;
using eCommerce.Presentation.Features.Inventory.Services.Services;

namespace eCommerce.Presentation.Features.Inventory.Services.Endpoints.V1.Delete;

internal sealed class DeleteServiceEndpoint : Endpoint<DeleteServiceRequest, Response>
{
    public override void Configure()
    {
        Delete($"{nameof(ModuleName.Inventory)}/{nameof(FeatureName.Service)}/{nameof(Delete)}");
        Permissions(SystemConstants.Security.Inventory.Services.Delete);
    }

    public override async Task HandleAsync(DeleteServiceRequest req, CancellationToken ct) =>
        Response = await Resolve<IServiceService>().DeleteAsync(req, ct);
}
