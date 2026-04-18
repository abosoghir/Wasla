using Wasla.Common.Email;
using Wasla.Common.FileServices;
using Wasla.Common.Handlers;
using Wasla.Persistence;
using Hangfire;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Wasla.Entities.Identity;

namespace Wasla;


public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services
            .AddAddCorsConfig()
            .AddAuthConfig(configuration)
            .AddDatabaseConfig(configuration)
            .AddSwaggerServices()
            .AddMapsterConfig()
            .AddFluentValidationConfig()
            .AddMediatRConfig()
            .AddBackgroundJobsConfig(configuration);

        // Register the generic repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFileService, FileService>();

        // inject services here 
        services.AddScoped<IEmailSender, EmailService>();
        services.AddScoped<EmailHelper>();

        // BackgroundJobs 
        //services.AddScoped<BackgroundJobs>();


        services.AddHttpContextAccessor(); // to be able to access HttpContext in services then to reach header then get original url and more 
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        // Exception Handling 
        // the order is important because it will check in order
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        

        // services.AddHealthChecks(); // to add health checks 

        // Rate Limiting 

        return services;
    }

    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? // how to read something in appsettings.json
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


        return services;
    }

    private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        // services.AddOpenApi();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

        //services.AddSwaggerGen(c =>
        //{
        //    c.TagActionsBy(api =>
        //    {
        //        var method = api.HttpMethod;
        //        return new[] { method ?? "OTHER" };
        //    });

        //    c.DocInclusionPredicate((_, _) => true);
        //});

        services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen();

        services.AddSwaggerGen(c =>
        {
            c.TagActionsBy(api =>
            {
                var controllerNamespace = api.ActionDescriptor
                    .EndpointMetadata
                    .OfType<ApiControllerAttribute>()
                    .FirstOrDefault();

                var fullName = api.ActionDescriptor.DisplayName;

                var featureName = fullName?
                    .Split('.')
                    .SkipWhile(x => x != "Features")
                    .Skip(1)
                    .FirstOrDefault();

                return new[] { featureName ?? "Other" };
            });

            c.DocInclusionPredicate((_, _) => true);
        });

        
        return services;
    }

    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly()); // for scanning classes that implement IRegister
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }

    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {

        services
             //.AddFluentValidationAutoValidation()
             .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // this will scan and register all validators in the assembly that inherit from AbstractValidator<T>

        return services;
    }

    private static IServiceCollection AddMediatRConfig(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            // Tell MediatR where to find your Handlers
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationHandler<,>)
        );

        return services;
    }

    private static IServiceCollection AddAddCorsConfig(this IServiceCollection services)
    {
        // CORS must use AllowCredentials() for SignalR WebSocket authentication.
        // AllowAnyOrigin() is NOT compatible with AllowCredentials().
        // SetIsOriginAllowed(_ => true) allows all origins while supporting credentials.
        // For production, replace with explicit origins: .WithOrigins("https://yourfrontend.com")
        services.AddCors(options =>
                  options.AddPolicy("AllowSpecificOrigins", builder =>
                      builder
                          .SetIsOriginAllowed(_ => true)
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials()
                  )
        );

        return services;
    }

    private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
     IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
        // to use postgresql database for hangfire, you can change it to use sql server or any other database by changing the provider and the connection string

        services.AddHangfireServer();

        return services;
    }

    private static IServiceCollection AddAuthConfig(this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();


        services.AddSingleton<IJwtProvider, JwtProvider>();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
            };

            // ─── SignalR JWT Support ──────────────────────────────
            // WebSockets cannot send custom headers, so the JWT token
            // is sent via the query string: ?access_token=xxx
            // This event extracts it and sets the context token.
            o.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    // Only extract from query string for hub endpoints
                    if (!string.IsNullOrEmpty(accessToken) &&
                        path.StartsWithSegments("/hubs"))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                }
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 6;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });

        var test = new
        {
            IssuerSigningKey = jwtSettings?.Key!,
            ValidIssuer = jwtSettings?.Issuer,
            ValidAudience = jwtSettings?.Audience,
        };

        return services;
    }

}