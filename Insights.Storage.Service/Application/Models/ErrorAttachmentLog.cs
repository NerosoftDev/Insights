using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Error attachment log.
/// </summary>
[JsonObject("errorAttachment")]
public class ErrorAttachmentLog : Log
{
    /// <summary>
    /// Gets or sets error attachment identifier.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public System.Guid Id { get; set; }

    /// <summary>
    /// Gets or sets error log identifier to attach this log to.
    /// </summary>
    [JsonProperty(PropertyName = "errorId")]
    public System.Guid ErrorId { get; set; }

    /// <summary>
    /// Gets or sets content type (text/plain for text).
    /// </summary>
    [JsonProperty(PropertyName = "contentType")]
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets file name.
    /// </summary>
    [JsonProperty(PropertyName = "fileName")]
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets data encoded as base 64.
    /// </summary>
    [JsonProperty(PropertyName = "data")]
    public byte[] Data { get; set; }
}