namespace eCommerce.Persistence;

public static class Registration
{
    public static IServiceCollection RegisterePersistence(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        #region Env Configurations
        var root = Directory.GetCurrentDirectory();
        var parentRoot = Directory.GetParent(root);
        var dotenv = Path.Combine(parentRoot.FullName, ".env");
        Env.Load(dotenv);
        #endregion

        #region DbContext Configurations
        services.AddDbContext<IeCommerceDbContext, eCommerceDbContext>(
            options =>
            {
                options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
                options.AddInterceptors(new CustomSaveChangesInterceptor());
            },
            ServiceLifetime.Scoped
        );

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
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
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
                jwtBearerOptions.Authority = config["Authentication:Authority"];
                jwtBearerOptions.TokenValidationParameters.ValidateAudience = false;
                jwtBearerOptions.TokenValidationParameters.ValidateIssuer = true;
                jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey = false;
                jwtBearerOptions.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config.GetValue<string>(
                        $"{nameof(JwtSettings)}:{nameof(JwtSettings.Issuer)}"
                    ),
                    ValidAudience = config.GetValue<string>(
                        $"{nameof(JwtSettings)}:{nameof(JwtSettings.Audience)}"
                    ),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            config.GetValue<string>(
                                $"{nameof(JwtSettings)}:{nameof(JwtSettings.Secret)}"
                            )
                        )
                    ),
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
        #endregion

        #region Repositories
        services
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped<IUserRepository, UserRepository>();
        #endregion

        return services;
    }
}
