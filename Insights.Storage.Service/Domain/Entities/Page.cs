namespace Nerosoft.Insights.Storage.Domain;

public class Page : Log
{
    public virtual Guid? SessionId { get; set; }
    
    public string Name { get; set; }
}