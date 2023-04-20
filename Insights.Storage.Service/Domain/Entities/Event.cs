namespace Nerosoft.Insights.Storage.Domain;

public class Event : Log
{
    public virtual Guid? SessionId { get; set; }
    
    public string Name { get; set; }
}