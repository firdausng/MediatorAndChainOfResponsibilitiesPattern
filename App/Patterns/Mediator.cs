namespace App.Patterns;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse?> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TResponse : notnull where TRequest: IRequest
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = request.GetType();
        var requestHandler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var middlewareList = scope.ServiceProvider.GetRequiredService<IEnumerable<IMiddleware>>();

        var result = await middlewareList
            .Reverse()
            .Aggregate(
                Handler,
                (next, pipeline)
                    => ()
                        => pipeline.Handle(request, cancellationToken, next)
            )();
        return result;

        Task<TResponse> Handler() => requestHandler.Handle(request, cancellationToken);
    }

    public async Task Publish<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TResponse : notnull where TRequest: IRequest
    {
        using var scope = _serviceProvider.CreateScope();
        var notificationHandlerList = scope.ServiceProvider
            .GetRequiredService<IEnumerable<INotificationHandler<TRequest, TResponse>>>();
        var middlewareList = scope.ServiceProvider.GetRequiredService<IEnumerable<IMiddleware>>();

        var handlerTaskList = new List<Task<TResponse>>();
        foreach (var notificationHandler in notificationHandlerList)
        {
            Task<TResponse> Handler() => notificationHandler.Handle(request, cancellationToken);
            var middlewares = middlewareList.Reverse().ToList();
            var result = middlewares
                .Aggregate(
                    Handler,
                    (next, pipeline)
                        => ()
                            => pipeline.Handle(request, cancellationToken, next)
                )();
            handlerTaskList.Add(result);
        }

        await Task.WhenAll(handlerTaskList);
    }
}