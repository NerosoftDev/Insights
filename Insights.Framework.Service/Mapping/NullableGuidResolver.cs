using AutoMapper;

namespace Nerosoft.Insights.Framework;

public class NullableGuidResolver<TSource, TDestination> : IMemberValueResolver<TSource, TDestination, Guid?, Guid>
{
    public Guid Resolve(TSource source, TDestination destination, Guid? sourceMember, Guid destMember, ResolutionContext context)
    {
        return sourceMember ?? Guid.NewGuid();
    }
}