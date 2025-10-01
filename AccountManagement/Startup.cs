using AccountManagement.API.Data;
using AccountManagement.API.Repositories;
using AccountManagement.API.Validation;
using AccountManagement.Extensions;
using AccountManagement.Repositories;
using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IO;
using System.Text;

namespace AccountManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Logging, CORS, IIS integration
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();

            // DbContexts
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();

            // JWT Token service
            services.AddScoped<JwtTokenService>();
           



            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();

            // Authentication
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Account Management API",
                    Version = "v1"
                });
            });

            // Controllers & API behavior
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Management API v1");
                c.RoutePrefix = "swagger"; // Swagger available at root
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
