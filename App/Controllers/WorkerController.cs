using System.Threading.Channels;
using App.Patterns.Basic;
using App.Patterns.Workers;
using App.Workers;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkerController: Controller
{
    private readonly WorkerChannel _channel;

    public WorkerController(WorkerChannel channel)
    {
        _channel = channel;
    }
    
    [HttpPost("publish", Name = "PostWorker")]
    public async Task<IActionResult> Post(WorkerRequest request, CancellationToken cancellationToken)
    {   
        await _channel.Writer.WriteAsync((PublishType.Send, request), cancellationToken);
        
        return Ok();
    }
    
    [HttpPost("notify", Name = "NotifyWorker")]
    public async Task<IActionResult> NotifyWebhook(WorkerRequest request, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync((PublishType.Notify, request), cancellationToken);
        return Ok();
    }
}