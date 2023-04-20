using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// String property.
/// </summary>
[JsonObject("string")]
public class StringTypedProperty : TypedProperty
{
    /// <summary>
    /// Gets or sets string property value.
    /// </summary>
    [JsonProperty(PropertyName = "value")]
    public string Value { get; set; }
}