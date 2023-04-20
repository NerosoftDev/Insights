using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage.Models;

/// <summary>
/// Stack frame definition for any platform.
/// </summary>
public class StackFrame
{
    /// <summary>
    /// Gets or sets frame address.
    /// </summary>
    [JsonProperty(PropertyName = "address")]
    public string Address { get; set; }

    /// <summary>
    /// Gets or sets symbolized code line
    /// </summary>
    [JsonProperty(PropertyName = "code")]
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the fully qualified name of the Class containing the
    /// execution point represented by this stack trace element.
    /// </summary>
    [JsonProperty(PropertyName = "className")]
    public string ClassName { get; set; }

    /// <summary>
    /// Gets or sets the name of the method containing the execution point
    /// represented by this stack trace element.
    /// </summary>
    [JsonProperty(PropertyName = "methodName")]
    public string MethodName { get; set; }

    /// <summary>
    /// Gets or sets the line number of the source line containing the
    /// execution point represented by this stack trace element.
    /// </summary>
    [JsonProperty(PropertyName = "lineNumber")]
    public int? LineNumber { get; set; }

    /// <summary>
    /// Gets or sets the name of the file containing the execution point
    /// represented by this stack trace element.
    /// </summary>
    [JsonProperty(PropertyName = "fileName")]
    public string FileName { get; set; }
}