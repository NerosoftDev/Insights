using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

public class TypedProperty
{
    /// <summary>
    /// Gets or sets property key.
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
}