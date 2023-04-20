using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Handled Error log for managed platforms (such as Xamarin, Unity,
/// Android Dalvik/ART)
/// </summary>
[JsonObject("handledError")]
public class HandledErrorLog : LogWithProperties
{
    /// <summary>
    /// Gets or sets unique identifier for this Error.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public System.Guid? Id { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "binaries")]
    public IList<Binary> Binaries { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "exception")]
    public Exception Exception { get; set; }
}