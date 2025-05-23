﻿using eCommerce.Presentation.Features.Identity.Users.Service;

namespace eCommerce.Presentation.Features.Identity.Users.Endpoints.V1.Authenticate;

public class AuthenticateUserEndpoint : Endpoint<AuthenticateUserRequest, Response>
{
    public override void Configure()
    {
        Version(1);
        Post($"{ModuleName.Identity}/{EntityName.User}/{nameof(Authenticate)}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthenticateUserRequest req, CancellationToken ct)
    {
        Response = await Resolve<IUserService>().AuthenticateAsync(req, ct);
    }
}
