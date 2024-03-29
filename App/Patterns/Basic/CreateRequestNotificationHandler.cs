namespace App.Patterns.Basic;

public class CreateRequestNotificationHandler : INotificationHandler<CreateRequest, Unit>
{
    private readonly ILogger<CreateRequestNotificationHandler> _logger;

    public CreateRequestNotificationHandler(ILogger<CreateRequestNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("start notify CreateWebhookNotificationHandler");
        var result = Task.FromResult(new Unit());
        _logger.LogInformation("done notify CreateWebhookNotificationHandler");
        return result;
    }
}