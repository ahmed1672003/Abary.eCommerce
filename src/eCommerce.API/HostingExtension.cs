using eCommerce.Domain.Constants;
using eCommerce.Persistence.Middlewares.Identity;

namespace eCommerce.API;

public static class HostingExtension
{
    public static async Task<WebApplication> RegisterApiAsync(
        this IServiceCollection service,
        WebApplicationBuilder builder
    )
    {
        SystemConstants.SetEnvironmentVariables();
        service.AddHttpContextAccessor();
        service.AddAuthentication();
        service.AddCors(options =>
        {
            options.AddPolicy(
                "All",
                policy =>
                {
                    policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                }
            );
        });

        service
            .RegisterDomain()
            .RegisterPresentation()
            .AddEndpointsApiExplorer()
            .AddControllers()
            .AddApplicationPart(typeof(Presentation.Registeration).Assembly)
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );

        await service.RegisterePersistence(builder.Configuration);
        return builder.Build();
    }

    public static WebApplication HostServices(this WebApplication app)
    {
        app.UseCors("All");
        app.UsePresentation();
        app.ConfigureExceptionHandlingMiddleware();
        app.UseMiddleware<TokenMiddleware>();
        app.UseAuthentication().UseAuthorization();
        return app;
    }
}
