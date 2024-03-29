namespace App.Patterns.Basic;

public class CreateRequestHandler : IRequestHandler<CreateRequest, Unit>
{
    private readonly ILogger<CreateRequestHandler> _logger;

    public CreateRequestHandler(ILogger<CreateRequestHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(CreateRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("start send request");
        var result = Task.FromResult(new Unit());
        _logger.LogInformation("done send request");
        return result;
    }
}