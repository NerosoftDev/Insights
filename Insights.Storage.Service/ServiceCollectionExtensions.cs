using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nerosoft.Insights.Framework;
using Nerosoft.Insights.Storage.Application;
using Nerosoft.Insights.Storage.Domain;

namespace Nerosoft.Insights.Storage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMysqlStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddPooledDbContextFactory<EfCoreDataContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), builder =>
            {
                builder.EnableRetryOnFailure(3);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        });
        services.TryAddScoped<DbContext>(provider =>
        {
            return provider.GetRequiredService<IDbContextFactory<EfCoreDataContext>>().CreateDbContext();
        });
        return services;
    }

    public static IServiceCollection AddMssqlStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddPooledDbContextFactory<EfCoreDataContext>(options =>
        {
            options.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(3);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        });
        services.TryAddScoped<DbContext>(provider =>
        {
            return provider.GetRequiredService<IDbContextFactory<EfCoreDataContext>>().CreateDbContext();
        });
        return services;
    }

    public static IServiceCollection AddPgsqlStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepository<,>));
        services.AddPooledDbContextFactory<EfCoreDataContext>(options =>
        {
            options.UseNpgsql(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(3);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
        });
        services.TryAddScoped<DbContext>(provider =>
        {
            return provider.GetRequiredService<IDbContextFactory<EfCoreDataContext>>().CreateDbContext();
        });
        return services;
    }

    public static IServiceCollection AddMongoStorage(this IServiceCollection services, string connectionString)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(MongoRepository<,>));
        services.AddMongoDatabaseContext<MongoDataContext>(options =>
        {
            options.UseConnection(connectionString);
            options.UseDatabase("insights");
        });
        return services;
    }

    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddScoped<IStorageService, StorageService>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
        });
        return services;
    }
}