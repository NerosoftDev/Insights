namespace Nerosoft.Insights.Storage.Domain;

public class Binary
{
    public string Id { get; set; }

    public string StartAddress { get; set; }

    public string EndAddress { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public string Architecture { get; set; }

    public long? PrimaryArchitectureId { get; set; }

    public long? ArchitectureVariantId { get; set; }
}