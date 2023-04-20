using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

public class LogWithProperties : Log
{
    /// <summary>
    /// Gets or sets additional key/value pair parameters.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "properties")]
    public IDictionary<string, string> Properties { get; set; }
}