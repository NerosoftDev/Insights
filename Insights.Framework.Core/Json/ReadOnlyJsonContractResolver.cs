using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Nerosoft.Insights.Framework;

/// <summary>
/// JSON contract resolver that ignores read-only properties during serialization.
/// </summary>
public class ReadOnlyJsonContractResolver : DefaultContractResolver
{
    /// <summary>
    /// Creates a JsonProperty for the given MemberInfo.
    /// </summary>
    /// <param name="member">The member to create a JsonProperty for.</param>
    /// <param name="memberSerialization">The member's parent MemberSerialization.</param>
    /// <returns>A created JsonProperty for the given MemberInfo.</returns>
    protected override JsonProperty CreateProperty(
        MemberInfo member,
        MemberSerialization memberSerialization)
    {
        JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
        var propertyInfo = member as PropertyInfo;

        if (propertyInfo != null)
        {
            jsonProperty.ShouldSerialize = t => 
                (propertyInfo.SetMethod != null && !propertyInfo.SetMethod.IsPrivate) || 
                (propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsStatic);
        }

        return jsonProperty;
    }
}