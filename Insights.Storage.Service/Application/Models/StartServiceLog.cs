using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Describe a AppCenter.Start API call from the SDK.
/// </summary>
[JsonObject("startService")]
public class StartServiceLog : Log
{
    /// <summary>
    /// Gets or sets the list of services of the AppCenter Start API call.
    /// </summary>
    [JsonProperty(PropertyName = "services")]
    public IList<string> Services { get; set; }
}