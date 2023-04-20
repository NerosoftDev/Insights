﻿using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nerosoft.Insights.Storage;

public class LogJsonConverter : JsonConverter
{
    private readonly Dictionary<string, Type> _logTypes = new();
    private readonly object _jsonConverterLock = new();
    private static readonly JsonSerializerSettings _serializationSettings;

    private const string TypeIdKey = "type";

    static LogJsonConverter()
    {
        _serializationSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize
        };
    }

    public void AddLogType(string typeName, Type type)
    {
        lock (_jsonConverterLock)
        {
            _logTypes[typeName] = type;
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(ILog).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        Type logType;
        var jsonObject = JObject.Load(reader);
        var typeName = jsonObject.GetValue(TypeIdKey)?.ToString();
        lock (_jsonConverterLock)
        {
            if (typeName == null || !_logTypes.ContainsKey(typeName))
            {
                throw new JsonReaderException("Could not identify type of log");
            }

            logType = _logTypes[typeName];
            jsonObject.Remove(TypeIdKey);
        }

        return jsonObject.ToObject(logType);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var info = value.GetType().GetTypeInfo();
        var attribute = info.GetCustomAttribute(typeof(JsonObjectAttribute)) as JsonObjectAttribute;
        if (attribute == null)
        {
            throw new JsonWriterException("Cannot serialize log; Log type is missing JsonObjectAttribute");
        }

        var jsonText = JsonConvert.SerializeObject(value, _serializationSettings);
        var jsonObject = JObject.Parse(jsonText);
        jsonObject.Add(TypeIdKey, JToken.FromObject(attribute.Id));
        writer.WriteRawValue(jsonObject.ToString());
    }
}