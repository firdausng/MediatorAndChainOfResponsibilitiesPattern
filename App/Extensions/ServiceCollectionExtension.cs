using System.Threading.Channels;
using App.Patterns.Basic;
using App.Patterns.Workers;
using App.Workers;
using IMiddleware = App.Patterns.IMiddleware;

namespace App.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRequestWorker(this IServiceCollection services)
    {
        services.AddHostedService<RequestWorker>();
        services.AddSingleton<WorkerChannel>();
        
        return services;
    }
    
    public static IServiceCollection AddPattern(this IServiceCollection services)
    {
        services.AddTransient(typeof(IMiddleware), typeof(FirstLogMiddleware));
        services.AddTransient(typeof(IMiddleware), typeof(SecondLogMiddleware));

        services.AddTransient<IMediator, Mediator>();
        // services.AddTransient<IRequestHandler<IRequest, Unit>>();
        services.AddTransient<IRequestHandler<CreateRequest, Unit>, CreateRequestHandler>();
        services.AddTransient<IRequestHandler<WorkerRequest, Unit>, WorkerRequestHandler>();
        return services;
    }
}
