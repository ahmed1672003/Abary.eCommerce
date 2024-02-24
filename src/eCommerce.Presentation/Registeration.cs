using System.Reflection;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Persistence.Middlewares.ExceptionHandling;
using eCommerce.Presentation.Features.Identity.Users.DaoService;
using eCommerce.Presentation.Features.Identity.Users.Service;
using eCommerce.Presentation.Json.Service;
using eCommerce.Presentation.Jwt.Service;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Presentation;

public static class Registeration
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<ExceptionMiddleware>();

        // Mapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // DaoService
        services.AddScoped<IUserDaoService, UserDaoService>();

        // Service
        services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<UserManager<User>>()
            .AddScoped<RoleManager<Role>>()
            .AddScoped<SignInManager<User>>()
            .AddScoped<IJsonService, JsonService>();

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
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            c.Endpoints.ShortNames = true;
            //c.Endpoints.Configurator = ep =>
            //{
            //    ep.PostProcessors(Order.After, new ErrorLoggerProcessor());
            //    ep.PreProcessors(Order.Before, new LocalizationProcessor());
            //};
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
                    Message = $"{errorMsg.Value}"
                };
            };
        });
        app.UseOpenApi();
        app.UseSwaggerGen();
        app.UseSwaggerUi();
        return app;
    }
}
