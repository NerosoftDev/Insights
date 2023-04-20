using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

public class LogContainer
{
    /// <summary>
    /// Gets or sets the list of logs
    /// </summary>
    [JsonProperty(PropertyName = "logs")]
    public IList<Log> Logs { get; set; }
}