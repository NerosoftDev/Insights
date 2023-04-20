using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Long property (64 bits signed integer).
/// </summary>
[JsonObject("long")]
public class LongTypedProperty : TypedProperty
{
    /// <summary>
    /// Gets or sets long property value.
    /// </summary>
    [JsonProperty(PropertyName = "value")]
    public long Value { get; set; }
}