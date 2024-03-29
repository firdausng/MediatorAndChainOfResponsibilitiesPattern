namespace App.Patterns.Basic;
public class CreateSecondRequestNotificationHandler : INotificationHandler<CreateRequest, Unit>
{
    private readonly ILogger<CreateSecondRequestNotificationHandler> _logger;

    public CreateSecondRequestNotificationHandler(ILogger<CreateSecondRequestNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("start notify CreateWebhookNotificationHandler2");
        var result = Task.FromResult(new Unit());
        _logger.LogInformation("done notify CreateWebhookNotificationHandler2");
        return result;
    }
}