using System.Threading.Channels;
using App.Patterns.Workers;

namespace App.Workers;

public class RequestWorker : BackgroundService
{
    private readonly ILogger<RequestWorker> _logger;
    private readonly WorkerChannel _channel;
    private readonly IMediator _mediator;

    public RequestWorker(ILogger<RequestWorker> logger, WorkerChannel channel, IMediator mediator)
    {
        _logger = logger;
        _channel = channel;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            }

            var request = await _channel.Reader.ReadAsync(stoppingToken);

            switch (request.publishType)
            {
                case PublishType.Send:
                    await _mediator.Send<WorkerRequest, Unit>(request.workerRequest, stoppingToken);
                    break;
                case PublishType.Notify:
                    await _mediator.Publish<WorkerRequest, Unit>(request.workerRequest, stoppingToken);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }
}