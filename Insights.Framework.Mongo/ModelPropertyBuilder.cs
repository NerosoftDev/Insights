namespace Nerosoft.Insights.Framework;

public class ModelPropertyBuilder
{
    internal ModelPropertyBuilder(string propertyName, Type propertyType)
    {
        PropertyName = propertyName;
        PropertyType = propertyType;
    }

    internal string PropertyName { get; }

    internal Type PropertyType { get; }

    internal string ElementName { get; private set; }

    public ModelPropertyBuilder HasElementName(string elementName)
    {
        ElementName = elementName;
        return this;
    }
}