namespace Nerosoft.Insights.Framework;

public interface IObjectMapper
{
    TTarget ProjectAs<TSource, TTarget>(TSource source);

    object ProjectAs<TSource>(TSource source, Type targetType);
}
