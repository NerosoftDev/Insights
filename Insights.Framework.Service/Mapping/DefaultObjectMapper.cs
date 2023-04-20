using AutoMapper;

namespace Nerosoft.Insights.Framework;

public class DefaultObjectMapper : IObjectMapper
{
    private readonly IMapper _mapper;

    public DefaultObjectMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TTarget ProjectAs<TSource, TTarget>(TSource source)
    {
        return _mapper.Map<TTarget>(source);
    }

    public object ProjectAs<TSource>(TSource source, Type targetType)
    {
        return _mapper.Map(source, typeof(TSource), targetType);
    }
}