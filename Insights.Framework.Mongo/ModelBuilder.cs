using MongoDB.Bson.Serialization;

namespace Nerosoft.Insights.Framework;

public class ModelBuilder
{
    internal Action<Type, ModelProfile> OnConfigure { get; set; }

    public ModelBuilder For<T>(Action<ModelProfile<T>> buildAction)
        where T : class
    {
        var profile = new ModelProfile<T>();
        buildAction(profile);
        OnConfigure(typeof(T), profile);

        BsonClassMap.RegisterClassMap<T>(map =>
        {
            map.AutoMap();

            map.SetIgnoreExtraElements(true);
            if (!string.IsNullOrEmpty(profile.KeyName))
            {
                var type = profile.KeyType ?? typeof(T).GetProperty(profile.KeyName)?.PropertyType;
                map.MapIdProperty(profile.KeyName).SetSerializer(new ObjectIdSerializer(type));
            }

            foreach (var (name, property) in profile.Properties)
            {
                var member = map.MapProperty(name);
                if (string.IsNullOrEmpty(property.ElementName))
                {
                    member.SetElementName(property.ElementName);
                }
            }
        });
        return this;
    }
}