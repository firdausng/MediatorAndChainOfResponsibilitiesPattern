using App.Patterns.Basic;

namespace App.Patterns;

public class SecondLogMiddleware : IMiddleware

{
    private readonly ILogger<SecondLogMiddleware> _logger;

    public SecondLogMiddleware(ILogger<SecondLogMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle<TResponse>(IRequest request, CancellationToken cancellationToken,
        Func<Task<TResponse>> next) where TResponse : notnull
    {
        _logger.LogInformation("start second LogMiddleware");
        if (request is CreateRequest r2) _logger.LogInformation(r2.Name);

        var a = await next();
        _logger.LogInformation("end second LogMiddleware");
        return a;
    }
}