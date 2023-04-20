using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

public class Log : ILog
{
    /// <summary>
    /// Gets or sets log timestamp, example: '2017-03-13T18:05:42Z'.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "timestamp")]
    public DateTime? Timestamp { get; set; }

    /// <summary>
    /// Gets or sets when tracking an analytics session, logs can be part
    /// of the session by specifying this identifier.
    /// This attribute is optional, a missing value means the session
    /// tracking is disabled (like when using only error reporting
    /// feature).
    /// Concrete types like StartSessionLog or PageLog are always part of a
    /// session and always include this identifier.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "sid")]
    public Guid? Sid { get; set; }

    /// <summary>
    /// Gets or sets optional string used for associating logs with users.
    ///
    /// </summary>
    [JsonProperty(PropertyName = "userId")]
    public string UserId { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty(PropertyName = "device")]
    public Device Device { get; set; }
}