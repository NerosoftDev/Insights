using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nerosoft.Insights.Framework;

namespace Nerosoft.Insights.Storage.Domain;

public class EfCoreRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly DbContext _context;

    public EfCoreRepository(DbContext context)
    {
        _context = context;
    }

    public async Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await _context.AddAsync(entity, cancellationToken);
        return entry.Entity.Id;
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.AddRangeAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var _ = _context.Update(entity);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(TKey key, Action<TEntity> update, CancellationToken cancellationToken = default)
    {
        var entity = await _context.FindAsync<TEntity>(new[] { key }, cancellationToken);
        if (entity == null)
        {
            throw new DataNotFoundException();
        }

        update?.Invoke(entity);
        await UpdateAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.UpdateRange(entities);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var _ = _context.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        _context.RemoveRange(entities);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
    {
        var entity = await _context.FindAsync<TEntity>(new[] { key }, cancellationToken);
        if (entity == null)
        {
            throw new DataNotFoundException();
        }

        await DeleteAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
    {
        var entities = await _context.Set<TEntity>().Where(t => keys.Contains(t.Id)).ToListAsync(cancellationToken);
        await DeleteAsync(entities, cancellationToken);
    }

    public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return await _context.FindAsync<TEntity>(new[] { key }, cancellationToken);
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
    {
        Expression<Func<TEntity, bool>> predicate = t => keys.Contains(t.Id);
        return await FetchAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
                             .Where(predicate)
                             .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int size, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
                             .Where(predicate)
                             .OrderByDescending(t => t.Id)
                             .Skip(offset).Take(size)
                             .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().CountAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(true, cancellationToken);
    }
}