namespace Nerosoft.Insights.Storage.Domain;

public class Property
{
    public Guid Id { get; set; }

    public Guid LogId { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }
}