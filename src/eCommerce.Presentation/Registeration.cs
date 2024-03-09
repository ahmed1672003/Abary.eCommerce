using System.Reflection;
using eCommerce.Persistence.Middlewares;
using eCommerce.Presentation.Features.Identity.Users.DaoService;
using eCommerce.Presentation.Features.Identity.Users.Service;
using eCommerce.Presentation.Features.Inventory.Units.DaoService;
using eCommerce.Presentation.Features.Inventory.Units.Service;
using eCommerce.Presentation.Json.Service;
using eCommerce.Presentation.Jwt.Service;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Presentation;

public static class Registeration
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        // Mapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Fluent Validation

        // DaoService
        services.AddTransient<IUserDaoService, UserDaoService>();
        services.AddTransient<IUnitDaoService, UnitDaoService>();

        // Service
        services
            .AddTransient<IUserService, UserService>()
            .AddTransient<IUnitService, UnitService>()
            .AddTransient<IJwtService, JwtService>()
            .AddTransient<UserManager<User>>()
            .AddTransient<RoleManager<Role>>()
            .AddTransient<SignInManager<User>>()
            .AddTransient<IJsonService, JsonService>();

        services.AddScoped<TokenMiddleware>();
        services.AddScoped<ExceptionMiddleware>();

        // Fastendpoints
        services.AddFastEndpoints();

        // Swagger
        services.SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.DocumentName = "eCommerce Version 1.0";
                s.Title = "eCommerce";
                s.Version = "v1";
            };
            o.MaxEndpointVersion = 2;
            o.EnableJWTBearerAuth = true;
            o.ExcludeNonFastEndpoints = true;
            o.ShortSchemaNames = true;
            o.AutoTagPathSegmentIndex = 2;
        });

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseFastEndpoints(c =>
        {
            c.Versioning.Prefix = "V";
            c.Versioning.PrependToRoute = true;
            c.Endpoints.RoutePrefix = "API/eCommerce";
            c.Endpoints.ShortNames = true;
            //  c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //  c.Serializer.Options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            c.Errors.ResponseBuilder = (failures, ctx, statusCode) =>
            {
                var errorMsg = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(
                        keySelector: e => e.Key,
                        elementSelector: e =>
                            e.Select(m => m.ErrorMessage).ToArray().FirstOrDefault()
                    )
                    .FirstOrDefault();
                return new Response()
                {
                    IsSuccess = false,
                    StatusCode = statusCode,
                    Message = $"{errorMsg.Value}",
                };
            };
        });
        app.UseOpenApi();
        app.UseSwaggerGen();
        app.UseSwaggerUi();
        return app;
    }
}
