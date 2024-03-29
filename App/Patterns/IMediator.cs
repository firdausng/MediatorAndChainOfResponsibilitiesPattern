namespace App.Patterns;

public interface IMediator
{
    public Task<TResponse?> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TResponse : notnull where TRequest: IRequest;

    public Task Publish<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TResponse : notnull where TRequest: IRequest;
}