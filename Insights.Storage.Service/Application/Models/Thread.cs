using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Thread definition for any platform.
/// </summary>
public class Thread
{
    /// <summary>
    /// Gets or sets thread identifier.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets thread name.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets stack frames.
    /// </summary>
    [JsonProperty(PropertyName = "frames")]
    public IList<StackFrame> Frames { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "exception")]
    public Exception Exception { get; set; }
}