using App.Patterns.Basic;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestController : Controller
{
    private readonly IMediator _mediator;

    public RequestController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("publish", Name = "PostWebhook")]
    public async Task<IActionResult> Post(CreateRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send<CreateRequest, Unit>(request, cancellationToken);
        return Ok();
    }
    
    [HttpPost("notify", Name = "NotifyWebhook")]
    public async Task<IActionResult> NotifyWebhook(CreateRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Publish<CreateRequest, Unit>(request, cancellationToken);
        return Ok();
    }
}