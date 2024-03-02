using FastEndpoints;
using Microsoft.AspNetCore.Authentication;

namespace eCommerce.Persistence.Middlewares.Identity;

public sealed class TokenMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            var accessToken = await context.GetTokenAsync("access_token");
            if (accessToken != null)
            {
                var token = await context
                    .Resolve<IeCommerceDbContext>()
                    .Set<UserToken>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Value == accessToken);

                if (token == null)
                {
                    var statusCode = (int)CustomHttpStatusCode.LoginAgain;
                    var response = GetResponse(statusCode);
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(response);
                    context.MarkResponseStart();
                }
            }
            await next(context);
        }
        catch (Exception)
        {
            await next(context);
        }
    }

    private Response GetResponse(int httpStatusCode)
    {
        var message = "قم بتسجيل الدخول مرة أخري";
        return new Response
        {
            IsSuccess = false,
            StatusCode = httpStatusCode,
            Message = message
        };
    }
}
