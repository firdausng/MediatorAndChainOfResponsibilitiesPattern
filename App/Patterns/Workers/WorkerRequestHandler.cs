namespace App.Patterns.Workers;

public class WorkerRequestHandler : IRequestHandler<WorkerRequest, Unit>
{
    private readonly ILogger<WorkerRequestHandler> _logger;

    public WorkerRequestHandler(ILogger<WorkerRequestHandler> logger)
    {
        _logger = logger;
    }

    public Task<Unit> Handle(WorkerRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("start send worker request");
        var result = Task.FromResult(new Unit());
        _logger.LogInformation("done send worker request");
        return result;
    }
}