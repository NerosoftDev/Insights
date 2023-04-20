using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Double property.
/// </summary>
[JsonObject("double")]
public class DoubleTypedProperty : TypedProperty
{
    /// <summary>
    /// Gets or sets double property value.
    /// </summary>
    [JsonProperty(PropertyName = "value")]
    public double Value { get; set; }
}