namespace Nerosoft.Insights.Framework;

public class ModelKeyBuilder
{
    internal ModelKeyBuilder(string key, Type type)
    {
        Key = key;
        Type = type;
    }

    internal string Key { get; }

    internal Type Type { get; }

    internal bool AutoGenerateId { get; private set; } = true;

    public ModelKeyBuilder GenerateOnInsert(bool value)
    {
        AutoGenerateId = value;
        return this;
    }
}