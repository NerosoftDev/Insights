using Nerosoft.Insights.Framework;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDatabaseContext<TContext>(this IServiceCollection services, Action<MongoDbOptions> configure)
        where TContext : MongoDbContext
    {
        services.Configure(configure);
        services.AddScoped<MongoDbContext, TContext>(provider => provider.GetService<TContext>());
        services.AddScoped<TContext>();
        return services;
    }
}