using AutoMapper;

namespace Nerosoft.Insights.Framework;

public class NullableGuidConverter : IValueConverter<Guid?, Guid>
{
    public Guid Convert(Guid? sourceMember, ResolutionContext context)
    {
        return sourceMember ?? Guid.NewGuid();
    }
}