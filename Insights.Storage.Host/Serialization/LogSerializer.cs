using System.Reflection;
using Newtonsoft.Json;

namespace Nerosoft.Insights.Storage;

public static class LogSerializer
{
    private static readonly JsonSerializerSettings _serializationSettings;
    private static readonly LogJsonConverter _converter = new();

    static LogSerializer()
    {
        _serializationSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            //ContractResolver = new ReadOnlyJsonContractResolver(),
            Converters =
            {
                _converter
            }
        };
    }

    public static void RegisterType(Assembly assembly)
    {
        var types = assembly.GetTypes();
        RegisterType(types);
    }

    public static void RegisterType(params Type[] types)
    {
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<JsonObjectAttribute>();
            if (attribute == null)
            {
                continue;
            }

            RegisterType(attribute.Id, type);
        }
    }

    public static void RegisterType(string typeName, Type type)
    {
        _converter.AddLogType(typeName, type);
    }

    public static string Serialize<T>(T @object)
    {
        return JsonConvert.SerializeObject(@object, _serializationSettings);
    }

    public static T Deserialize<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, _serializationSettings);
    }
}