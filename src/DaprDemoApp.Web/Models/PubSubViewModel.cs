using DaprDemoApp.Common;

namespace DaprDemoApp.Web.Models;

public class PubSubViewModel
{
    public Message Message { get; set; } = new();

    public PublishStatus? Status { get; set; }
}
