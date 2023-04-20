using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Nerosoft.Insights.Storage.Domain;

public class GuidGenerator : ValueGenerator<Guid>
{
    public override Guid Next(EntityEntry entry)
    {
        return Guid.NewGuid();
    }

    public override bool GeneratesTemporaryValues => false;
}