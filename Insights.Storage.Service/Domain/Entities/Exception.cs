namespace Nerosoft.Insights.Storage.Domain;

public class Exception
{
    public string Type { get; set; }
    
    public string Message { get; set; }
    
    public string StackTrace { get; set; }
    
    public IList<StackFrame> Frames { get; set; }
    
    public IList<Exception> InnerExceptions { get; set; }
    
    public string WrapperSdkName { get; set; }
}