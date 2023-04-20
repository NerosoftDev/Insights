using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Page view log (as in screens or activities).
/// </summary>
[JsonObject("page")]
public class PageLog : LogWithProperties
{
    /// <summary>
    /// Gets or sets name of the page.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}