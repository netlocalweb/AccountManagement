using AccountManagement.Service;
using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;

namespace AccountManagement.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Account Management API",
                    Version = "v1",
                    Description = "AccountManagement API by LocalWeb",
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Enter 'Bearer {your token}'"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IAuthService, AuthService>();

        }
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration) => 
            services.AddDbContext<RepositoryContext>(opts => 
            opts.UseSqlServer(configuration.GetConnectionString("AccountManagementConnectionString"), b => b.MigrationsAssembly("AccountManagement"))
            );

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var buider = services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }



        //    public static void ConfigureDapperContext(this IServiceCollection services) =>
        //        services.AddSingleton<DapperContext>();

        //    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        //       services.AddScoped<IRepositoryManager, RepositoryManager>();

        //    public static void ConfigureDapperRepository(this IServiceCollection services) =>
        //        services.AddScoped<IDapperRepository, DapperRepository>();
    }
}
