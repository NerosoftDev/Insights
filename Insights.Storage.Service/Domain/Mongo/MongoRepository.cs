using System.ComponentModel;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using Nerosoft.Insights.Framework;

namespace Nerosoft.Insights.Storage.Domain;

public class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    private readonly MongoDataContext _context;

    public MongoRepository(MongoDataContext context)
    {
        _context = context;
    }

    public async Task<TKey> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var document = await _context.InsertAsync(entity, cancellationToken);
        var id = document["_id"];
        return Convert<TKey>(id);
    }

    public async Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.InsertAsync(entities, cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(ObjectId.Parse(entity.Id.ToString()), entity, cancellationToken);
    }

    public async Task UpdateAsync(TKey key, Action<TEntity> update, CancellationToken cancellationToken = default)
    {
        var id = ObjectId.Parse(key.ToString());
        var entity = await _context.FindAsync<TEntity>(id, cancellationToken);
        if (entity == null)
        {
            throw new DataNotFoundException();
        }

        update?.Invoke(entity);

        await _context.UpdateAsync(id, entity, cancellationToken);
    }

    public async Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.UpdateAsync(entities, t => t.Id, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync<TEntity>(ObjectId.Parse(entity.Id.ToString()), cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        var keys = entities.Select(t => t.Id);
        await DeleteAsync(keys, cancellationToken);
    }

    public async Task DeleteAsync(TKey key, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(new[] { key }, cancellationToken);
    }

    public async Task DeleteAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
    {
        await _context.DeleteAsync<TEntity>(t => keys.Contains(t.Id), cancellationToken);
    }

    public async Task<TEntity> FindAsync(TKey key, CancellationToken cancellationToken = default)
    {
        return await _context.FindAsync<TEntity>(ObjectId.Parse(key.ToString()), cancellationToken);
    }

    public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = await _context.FindAsync(predicate, null, cancellationToken);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(IEnumerable<TKey> keys, CancellationToken cancellationToken = default)
    {
        var ids = keys.Select(t => ObjectId.Parse(t.ToString()));
        var filter = Builders<TEntity>.Filter.In("_id", ids);
        var query = await _context.FindAsync(filter, null, cancellationToken);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = await _context.FindAsync(predicate, null, cancellationToken);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FetchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int size, CancellationToken cancellationToken = default)
    {
        var sort = Builders<TEntity>.Sort.Ascending("_id");
        var options = new FindOptions<TEntity> { Limit = size, Skip = offset, Sort = sort };
        var query = await _context.FindAsync(predicate, options, cancellationToken);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = _context.Find(predicate);
        var result = await query.CountDocumentsAsync(cancellationToken);
        return (int)result;
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var query = _context.Find(predicate);
        var result = await query.AnyAsync(cancellationToken);
        return result;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    private static TValue Convert<TValue>(string content)
    {
        return (TValue)TypeDescriptor.GetConverter(typeof(TValue))
                                     .ConvertFrom(content);
    }

    private static TValue Convert<TValue>(BsonValue id)
    {
        string content;
        if (id.IsGuid)
        {
            content = id.AsGuid.ToString();
        }
        else if (id.IsInt64)
        {
            content = id.AsInt64.ToString();
        }
        else if (id.IsInt32)
        {
            content = id.AsInt32.ToString();
        }
        else if (id.IsString)
        {
            content = id.AsString;
        }
        else if (id.IsObjectId)
        {
            content = id.AsObjectId.ToString();
        }
        else
        {
            content = id.ToString();
        }

        return Convert<TValue>(content);
    }
}