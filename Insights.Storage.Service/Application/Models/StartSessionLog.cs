using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Required explicit begin session log (a marker event for analytics
/// service).
/// </summary>
[JsonObject("startSession")]
public class StartSessionLog : Log
{
    /// <summary>
    /// Gets or sets distribution group identifier.
    /// </summary>
    [JsonProperty(PropertyName = "distributionGroupId")]
    public System.Guid? DistributionGroupId { get; set; }
}