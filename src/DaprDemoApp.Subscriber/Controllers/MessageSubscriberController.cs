using DaprDemoApp.Common;
using Microsoft.AspNetCore.Mvc;

namespace DaprDemoApp.Subscriber.Controllers;

[ApiController]
public class SubscriberController : ControllerBase
{
    private readonly DaprHttpClient daprHttpClient;
    private readonly ILogger<SubscriberController> logger;

    public SubscriberController(DaprHttpClient daprHttpClient, ILogger<SubscriberController> logger)
    {
        this.daprHttpClient = daprHttpClient;
        this.logger = logger;
    }

    [HttpPost("getmessage")]
    public async Task<IActionResult> Post([FromBody] CloudEvent<Message> cloudEvent)
    {
        var logInfo = $"Received message {cloudEvent.Data?.Content} from {cloudEvent.Source}";
        logger.LogInformation(message: logInfo);

        await daprHttpClient.InvokeBindingAsync("notification", new Payload { Operation = "create", Data = cloudEvent.Data?.Content });
        logInfo = $"Invoked smtp binding successfully";
        logger.LogInformation(message: logInfo);

        return Ok();
    }
}
