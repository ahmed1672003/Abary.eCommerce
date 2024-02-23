namespace eCommerce.Presentation.Identity.Features.User.Endpoints.V1.Create;

public sealed class CreateUserEndpoint : Endpoint<CreateUserRequest, Response>
{
    public override void Configure()
    {
        Get($"{ModuleName.Identity}/{FeatureName.User}/{nameof(Create)}");
        Version(1);
        Permissions();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct) =>
        Response = await Resolve<IUserService>().CreatAsync(req, ct);
}
