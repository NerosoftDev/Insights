using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Abstract error log.
/// </summary>
public class AbstractErrorLog : Log
{
    /// <summary>
    /// Gets or sets error identifier.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets process identifier.
    /// </summary>
    [JsonProperty(PropertyName = "processId")]
    public int ProcessId { get; set; }

    /// <summary>
    /// Gets or sets process name.
    /// </summary>
    [JsonProperty(PropertyName = "processName")]
    public string ProcessName { get; set; }

    /// <summary>
    /// Gets or sets parent's process identifier.
    /// </summary>
    [JsonProperty(PropertyName = "parentProcessId")]
    public int? ParentProcessId { get; set; }

    /// <summary>
    /// Gets or sets parent's process name.
    /// </summary>
    [JsonProperty(PropertyName = "parentProcessName")]
    public string ParentProcessName { get; set; }

    /// <summary>
    /// Gets or sets error thread identifier.
    /// </summary>
    [JsonProperty(PropertyName = "errorThreadId")]
    public long? ErrorThreadId { get; set; }

    /// <summary>
    /// Gets or sets error thread name.
    /// </summary>
    [JsonProperty(PropertyName = "errorThreadName")]
    public string ErrorThreadName { get; set; }

    /// <summary>
    /// Gets or sets if true, this error report is an application crash.
    /// Corresponds to the number of milliseconds elapsed between the time
    /// the error occurred and the app was launched.
    /// </summary>
    [JsonProperty(PropertyName = "fatal")]
    public bool Fatal { get; set; }

    /// <summary>
    /// Gets or sets timestamp when the app was launched, example:
    /// '2017-03-13T18:05:42Z'.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "appLaunchTimestamp")]
    public DateTime? AppLaunchTimestamp { get; set; }

    /// <summary>
    /// Gets or sets CPU architecture.
    /// </summary>
    [JsonProperty(PropertyName = "architecture")]
    public string Architecture { get; set; }
}