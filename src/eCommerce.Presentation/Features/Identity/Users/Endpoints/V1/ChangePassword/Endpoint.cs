﻿using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.ChangePassword;

public class ChangePasswordEndpoint : Endpoint<ChangePasswordRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Put($"{nameof(ModuleName.Identity)}/{nameof(FeatureName.User)}/{nameof(ChangePassword)}");
        Permissions();
    }

    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct) =>
        Response = await Resolve<IUserService>().ChangePasswordAsync(req, ct);
}
