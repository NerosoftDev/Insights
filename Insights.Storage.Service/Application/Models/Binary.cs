using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Binary (library) definition for any platform.
/// </summary>
public class Binary
{
    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "startAddress")]
    public string StartAddress { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "endAddress")]
    public string EndAddress { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "path")]
    public string Path { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "architecture")]
    public string Architecture { get; set; }

    /// <summary>
    /// Gets or sets CPU primary architecture.
    /// </summary>
    [JsonProperty(PropertyName = "primaryArchitectureId")]
    public long? PrimaryArchitectureId { get; set; }

    /// <summary>
    /// Gets or sets CPU architecture variant.
    /// </summary>
    [JsonProperty(PropertyName = "architectureVariantId")]
    public long? ArchitectureVariantId { get; set; }
}