using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using MyApp.Application.Common.Behaviors;


namespace MyApp.Application;


public static class ConfigureServices
{

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // NOTE: Do not register the Custom LoggingBehaviour, as this would cause a circular dependency for the service.
        //       It is automatically registered by the default ILogger
        // services.AddSingleton(typeof(Microsoft.Extensions.Logging.ILogger<>), typeof(LoggingBehaviour<>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    
    }

}
