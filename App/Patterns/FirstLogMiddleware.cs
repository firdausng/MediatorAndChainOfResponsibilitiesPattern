namespace App.Patterns;

public class FirstLogMiddleware : IMiddleware
{
    private readonly ILogger<FirstLogMiddleware> _logger;

    public FirstLogMiddleware(ILogger<FirstLogMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle<TResponse>(IRequest request, CancellationToken cancellationToken,
        Func<Task<TResponse>> next) where TResponse : notnull
    {
        _logger.LogInformation("start first LogMiddleware");
        var a = await next();
        _logger.LogInformation("end first LogMiddleware");
        return a;
    }
}