using DaprDemoApp.Common;
using DaprDemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace DaprDemoApp.Web.Controllers;

public class PubSubController : Controller
{
    private readonly DaprHttpClient daprHttpClient;
    private readonly ILogger<PubSubController> logger;

    public PubSubController(DaprHttpClient daprHttpClient, ILogger<PubSubController> logger)
    {
        this.daprHttpClient = daprHttpClient;
        this.logger = logger;
    }

    public IActionResult Index()
    {
        return View(new PubSubViewModel());
    }

    public async Task<IActionResult> Publish([FromForm] Message message)
    {
        try
        {
            await daprHttpClient.PublishAsync("pubsub", "messages", message);

            return View("Index", new PubSubViewModel { Message = new(), Status = new PublishStatus(true, "Message published successfully!") });
        }
        catch (HttpRequestException)
        {
            var model = new PubSubViewModel
            {
                Message = message,
                Status = new PublishStatus(false, "Unable to publish message. Try again sometime.")
            };

            return View("Index", model);
        }
    }
}
