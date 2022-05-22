using Newtonsoft.Json;

namespace DaprDemoApp.Common;

public class Message
{
    [JsonProperty("content")]
    public string Content { get; set; } = string.Empty;
}
