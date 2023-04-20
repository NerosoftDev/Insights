using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Log is used to update distribution group identifier for session (a
/// marker event for analytics service).
/// </summary>
[JsonObject("distributionStartSession")]
public class DistributionStartSessionLog : Log
{
    /// <summary>
    /// Gets or sets distribution group identifier.
    /// </summary>
    [JsonProperty(PropertyName = "distributionGroupId")]
    public System.Guid DistributionGroupId { get; set; }
}