using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Error log for Apple platforms.
/// </summary>
[JsonObject("appleError")]
public class AppleErrorLog : AbstractErrorLog
{
    /// <summary>
    /// Gets or sets CPU primary architecture.
    /// </summary>
    [JsonProperty(PropertyName = "primaryArchitectureId")]
    public long PrimaryArchitectureId { get; set; }

    /// <summary>
    /// Gets or sets CPU architecture variant.
    /// </summary>
    [JsonProperty(PropertyName = "architectureVariantId")]
    public long? ArchitectureVariantId { get; set; }

    /// <summary>
    /// Gets or sets path to the application.
    /// </summary>
    [JsonProperty(PropertyName = "applicationPath")]
    public string ApplicationPath { get; set; }

    /// <summary>
    /// Gets or sets OS exception type.
    /// </summary>
    [JsonProperty(PropertyName = "osExceptionType")]
    public string OsExceptionType { get; set; }

    /// <summary>
    /// Gets or sets OS exception code.
    /// </summary>
    [JsonProperty(PropertyName = "osExceptionCode")]
    public string OsExceptionCode { get; set; }

    /// <summary>
    /// Gets or sets OS exception address.
    /// </summary>
    [JsonProperty(PropertyName = "osExceptionAddress")]
    public string OsExceptionAddress { get; set; }

    /// <summary>
    /// Gets or sets exception type.
    /// </summary>
    [JsonProperty(PropertyName = "exceptionType")]
    public string ExceptionType { get; set; }

    /// <summary>
    /// Gets or sets exception reason.
    /// </summary>
    [JsonProperty(PropertyName = "exceptionReason")]
    public string ExceptionReason { get; set; }

    /// <summary>
    /// Gets or sets content of register that might contain last method
    /// call.
    /// </summary>
    [JsonProperty(PropertyName = "selectorRegisterValue")]
    public string SelectorRegisterValue { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "threads")]
    public IList<Thread> Threads { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "binaries")]
    public IList<Binary> Binaries { get; set; }

    /// <summary>
    /// Gets or sets registers.
    /// </summary>
    [JsonProperty(PropertyName = "registers")]
    public IDictionary<string, string> Registers { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "exception")]
    public Exception Exception { get; set; }
}