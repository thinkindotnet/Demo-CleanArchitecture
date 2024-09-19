using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MyApp.Application.Common.Interfaces;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Interceptors;
using MyApp.Infrastructure.Services;


namespace MyApp.Infrastructure;


public static class ConfigureServices
{

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {

        const string connectionStringName = "DefaultConnection";
        var connectionString 
            = configuration.GetConnectionString(connectionStringName)
              ?? throw new InvalidOperationException($"Connection String {connectionStringName} is not found.");

        services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

        if (environment.IsDevelopment())
        {
            services
                .AddDatabaseDeveloperPageExceptionFilter();
        }

        services
            .AddScoped<IApplicationDbContext>(serviceProvider =>
            {
                return serviceProvider.GetRequiredService<ApplicationDbContext>();
            });

        services
            .AddScoped<ApplicationDbContextInitializer>();

        services
            .AddScoped<AuditableEntitySaveChangesInterceptor>();

        services
            .AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services
            .AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
