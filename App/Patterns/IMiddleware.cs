namespace App.Patterns;

public interface IMiddleware
{
    Task<TResponse> Handle<TResponse>(IRequest request, CancellationToken cancellationToken,
        Func<Task<TResponse>> next) where TResponse : notnull;
}