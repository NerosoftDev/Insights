using System.Linq.Expressions;

namespace Nerosoft.Insights.Framework;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<object> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(object key, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(IEnumerable<object> keys, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int size, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(TKey key, Action<TEntity> update, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(TKey key, CancellationToken cancellationToken = default);

    Task DeleteAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default);

    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int size, CancellationToken cancellationToken = default);

    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}