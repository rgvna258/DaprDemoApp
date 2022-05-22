namespace DaprDemoApp.Web.Models;

public class PublishStatus
{
    public PublishStatus(bool isPublished, string description)
    {
        IsPublished = isPublished;
        Description = description;
    }

    public bool IsPublished { get; set; }

    public string Description { get; set; }
}