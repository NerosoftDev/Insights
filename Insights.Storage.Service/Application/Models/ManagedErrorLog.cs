using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Error log for managed platforms (such as Android Dalvik/ART).
/// </summary>
[JsonObject("managedError")]
public class ManagedErrorLog : AbstractErrorLog
{
    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "binaries")]
    public IList<Binary> Binaries { get; set; }

    /// <summary>
    /// Gets or sets unique ID for a Xamarin build or another similar
    /// technology.
    /// </summary>
    [JsonProperty(PropertyName = "buildId")]
    public string BuildId { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "exception")]
    public Exception Exception { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "threads")]
    public IList<Thread> Threads { get; set; }
}