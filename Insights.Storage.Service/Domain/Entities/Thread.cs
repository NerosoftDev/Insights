namespace Nerosoft.Insights.Storage.Domain;

public class Thread
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public IList<StackFrame> Frames { get; set; }
    
    public Exception Exception { get; set; }
}