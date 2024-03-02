using eCommerce.Persistence.Middlewares.Identity;

namespace eCommerce.API;

public static class HostingExtension
{
    public static async Task<WebApplication> RegisterApiAsync(
        this IServiceCollection service,
        WebApplicationBuilder builder
    )
    {
        service.AddHttpContextAccessor();
        service.AddAuthentication();
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
        app.UsePresentation();
        app.ConfigureExceptionHandlingMiddleware();
        app.UseMiddleware<TokenMiddleware>();
        app.UseAuthentication().UseAuthorization();
        return app;
    }
}
