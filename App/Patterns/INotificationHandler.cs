namespace App.Patterns;

public interface INotificationHandler<in TRequest, TResponse>
    where TRequest : IRequest where TResponse : notnull
{
    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}