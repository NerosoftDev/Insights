using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Event log.
/// </summary>
[JsonObject("event")]
public class EventLog : LogWithProperties
{
    /// <summary>
    /// Gets or sets unique identifier for this event.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public System.Guid Id { get; set; }

    /// <summary>
    /// Gets or sets name of the event.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets typed properties, replacing the generic properties if
    /// set.
    /// </summary>
    [JsonProperty(PropertyName = "typedProperties")]
    public IList<StringTypedProperty> TypedProperties { get; set; }
}