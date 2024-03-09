using eCommerce.Persistence.Seeders;
using eCommerce.Persistence.Seeding;
using eCommerce.Persistence.Settings;
using FastEndpoints;

namespace eCommerce.Persistence;

public static class Registration
{
    public static async Task<IServiceCollection> RegisterePersistence(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        services.AddDbContextPool<IeCommerceDbContext, eCommerceDbContext>(options =>
        {
            options.UseNpgsql(
                Environment.GetEnvironmentVariable(
                    nameof(SystemConstants.Database.Localhost.LOCALHOST_DATABASE_CONNECTION_STRING)
                )
            //Environment.GetEnvironmentVariable(
            //    nameof(SystemConstants.Database.Cloud.CLOUD_DATABASE_CONNECTION_STRING)
            //)
            );
            options.AddInterceptors(new CustomSaveChangesInterceptor());
            //   options.EnableSensitiveDataLogging();
        });
        services
            .AddIdentity<User, Role>(options =>
            {
                #region Email Options
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                #endregion

                #region Stores Options
                //options.Stores.ProtectPersonalData = true;
                #endregion

                #region Password Options
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                #endregion

                #region User Options
                options.User.RequireUniqueEmail = true;
                #endregion

                #region Lock Out Options
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                #endregion
            })
            .AddEntityFrameworkStores<eCommerceDbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtBearerOptions =>
            {
                var jwtSettings = services
                    .BuildServiceProvider()
                    .CreateScope()
                    .Resolve<JwtSettings>();
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateIssuer = jwtSettings.ValidateAudience,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    )
                };
            });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "V1",
                new()
                {
                    Title = "Abary_eCommerce",
                    Version = "1.0.0",
                    Contact = new OpenApiContact()
                    {
                        Email = "ahmedadel1672003@gmail.com",
                        Name = "Ahmed Adel"
                    }
                }
            );
            options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new()
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = JwtBearerDefaults.AuthenticationScheme,
                }
            );
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
        });
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddSingleton<JwtSettings>();

        var servicProvider = services.BuildServiceProvider();
        using (var scope = servicProvider.CreateScope())
        {
            using (var context = scope.Resolve<IeCommerceDbContext>())
            {
                var userManager = scope.Resolve<UserManager<User>>();
                var permissions = context.Set<Permission>();

                if (!await context.EnsureCreatedAsync())
                {
                    if (!await permissions.AnyAsync())
                    {
                        await PermissionSeeder.SeedPermissionsAsync(context);
                    }

                    if (!await userManager.Users.AnyAsync())
                    {
                        await UserSeeder.SeedAsync(context, userManager);
                    }
                }
            }
        }
        return services;
    }
}
