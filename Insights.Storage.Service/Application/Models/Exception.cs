using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Exception definition for any platform.
/// </summary>
public class Exception
{
    /// <summary>
    /// Gets or sets exception type.
    /// </summary>
    [JsonProperty(PropertyName = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets exception reason.
    /// </summary>
    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets raw stack trace. Sent when the frames property is
    /// either missing or unreliable.
    /// </summary>
    [JsonProperty(PropertyName = "stackTrace")]
    public string StackTrace { get; set; }

    /// <summary>
    /// Gets or sets stack frames. Optional.
    /// </summary>
    [JsonProperty(PropertyName = "frames")]
    public IList<StackFrame> Frames { get; set; }

    /// <summary>
    /// Gets or sets inner exceptions of this exception.
    /// </summary>
    [JsonProperty(PropertyName = "innerExceptions")]
    public IList<Exception> InnerExceptions { get; set; }

    /// <summary>
    /// Gets or sets name of the wrapper SDK that emitted this exeption.
    /// Consists of the name of the SDK and the wrapper platform, e.g.
    /// "appcenter.xamarin", "hockeysdk.cordova".
    ///
    /// </summary>
    [JsonProperty(PropertyName = "wrapperSdkName")]
    public string WrapperSdkName { get; set; }
}