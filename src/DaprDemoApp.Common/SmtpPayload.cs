namespace DaprDemoApp.Common
{
    public class SmtpPayload
    {
        public string? Operation { get; set; }
        public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
        public string? Data { get; set; }
    }
}