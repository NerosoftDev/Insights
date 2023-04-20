using System.Runtime.Serialization;

namespace Nerosoft.Insights.Framework;

public class DataNotFoundException : Exception
{
    public DataNotFoundException()
    {
    }

    protected DataNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public DataNotFoundException(string message)
        : base(message)
    {
    }

    public DataNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}