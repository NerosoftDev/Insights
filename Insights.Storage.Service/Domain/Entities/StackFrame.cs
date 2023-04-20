namespace Nerosoft.Insights.Storage.Domain;

public class StackFrame
{
    public string Address { get; set; }
    
    public string Code { get; set; }
    
    public string ClassName { get; set; }
    
    public string MethodName { get; set; }
    
    public int? LineNumber { get; set; }
    
    public string FileName { get; set; }
}