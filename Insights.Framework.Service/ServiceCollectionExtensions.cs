using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Nerosoft.Insights.Framework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddObjectMapper<TMapper>(this IServiceCollection services, Action<MapperConfigurationExpression> config = null)
        where TMapper : class, IObjectMapper
    {
        var expression = new MapperConfigurationExpression();
        config?.Invoke(expression);
        var mapperConfiguration = new MapperConfiguration(expression);

        var mapper = mapperConfiguration.CreateMapper();

        services.AddSingleton(mapper);
        services.AddSingleton<IObjectMapper, TMapper>();
        return services;
    }
}