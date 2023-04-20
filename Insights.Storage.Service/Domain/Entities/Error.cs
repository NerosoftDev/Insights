using System.Diagnostics.CodeAnalysis;

namespace Nerosoft.Insights.Storage.Domain;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
public class Error : Log
{
    public virtual Guid? SessionId { get; set; }
    
    public string Type { get; set; }

    public int? ProcessId { get; set; }

    public string ProcessName { get; set; }

    public int? ParentProcessId { get; set; }

    public string ParentProcessName { get; set; }

    public long? ErrorThreadId { get; set; }

    public string ErrorThreadName { get; set; }

    public bool Fatal { get; set; }

    public DateTime? AppLaunchTimestamp { get; set; }

    public string Architecture { get; set; }

    public IList<Binary> Binaries { get; set; }

    public string BuildId { get; set; }

    public Exception Exception { get; set; }

    public IList<Thread> Threads { get; set; }
}