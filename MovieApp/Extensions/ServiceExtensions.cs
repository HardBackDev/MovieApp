using Microsoft.AspNetCore.Identity;
using System.Reflection;
using LoggerService;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using FluentMigrator.Runner;
using Identity.Dapper.Postgres.Models;
using Identity.Dapper.Postgres;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository;
using MovieApp.Migrations;
using Contracts.RepositoryContracts;
using Repositories;
using Contracts.ServiceContracts;
using Service;
using MovieApp.Presentation.Filtres;
using Microsoft.AspNetCore.Hosting;

namespace MovieApp.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = """
                    JWT Authorization header using the Bearer scheme.  
                    Enter 'Bearer' [space] and then your token in the text input below.
                    Example: "Bearer 12345abcdef".
                    """,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddApplicationPart(typeof(MovieApp.Presentation.AssemblyReference).Assembly);
        }
        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();
                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.codemaze.hateoas+json");
                }
            });
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secret"];
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddDapperStores(configuration.GetConnectionString("npgConnection"))
            .AddDefaultTokenProviders();
        }

        public static void ConfigureFluentMigrator(this IServiceCollection services, IConfiguration configuration)
            => services.AddLogging(c =>
            c.AddFluentMigratorConsole())
            .AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddPostgres11_0().WithGlobalConnectionString(configuration
            .GetConnectionString("npgConnection"))
            .ScanIn(Assembly.GetExecutingAssembly())
            .For.Migrations());

        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination", "Location", "Accept-Ranges"));
        });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<DapperContext>();
            services.AddTransient<Database>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ValidationFilter>();
        }
    }
}
