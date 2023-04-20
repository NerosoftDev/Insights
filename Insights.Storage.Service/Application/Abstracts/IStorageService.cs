namespace Nerosoft.Insights.Storage.Application;

public interface IStorageService
{
    Task InsertAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class;
}