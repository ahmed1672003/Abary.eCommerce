using eCommerce.Domain.Constants;
using eCommerce.Persistence.Middlewares;

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

        service.AddScoped<TokenMiddleware>();
        service.AddScoped<ExceptionMiddleware>();

        await service.RegisterePersistence(builder.Configuration);
        return builder.Build();
    }

    public static WebApplication HostServices(this WebApplication app)
    {
        app.UseCors("All");
        // app.UseMiddleware<TokenMiddleware>();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UsePresentation();
        app.UseAuthentication().UseAuthorization();
        return app;
    }
}
