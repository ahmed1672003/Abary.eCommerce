using eCommerce.Persistence.Middlewares.ExceptionHandling;

namespace eCommerce.API;

public static class HostingExtension
{
    public static WebApplication RegisterApi(
        this IServiceCollection service,
        WebApplicationBuilder builder
    )
    {
        service.AddHttpContextAccessor();
        service.AddAuthentication();

        service
            .RegisterDomain()
            .RegisterPresentation(builder.Configuration)
            .RegisterePersistence(builder.Configuration)
            .AddEndpointsApiExplorer()
            .AddControllers()
            .AddApplicationPart(typeof(Presentation.Registeration).Assembly)
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
            );
        return builder.Build();
    }

    public static WebApplication HostServices(this WebApplication app)
    {
        app.UsePresentation();
        app.ConfigureExceptionHandlingMiddleware();
        app.UseAuthentication().UseAuthorization();
        return app;
    }
}
