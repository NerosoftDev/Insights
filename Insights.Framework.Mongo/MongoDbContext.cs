using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Nerosoft.Insights.Framework;

public abstract class MongoDbContext
{
    private readonly MongoDbOptions _options;
    private readonly IMongoDatabase _database;
    private readonly ModelProfileContainer _container;

    private readonly MongoCollectionSettings _collectionSettings = new() { AssignIdOnInsert = false };

    protected MongoDbContext(IOptions<MongoDbOptions> options)
    {
        _container = ModelProfileContainer.GetInstance(OnModelCreating);
        _options = options.Value;
        var client = new MongoClient(_options.ConnectionString);
        _database = client.GetDatabase(_options.Database);
    }

    protected virtual void OnModelCreating(ModelBuilder builder)
    {
    }

    public IMongoCollection<T> Collection<T>()
        where T : class
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        return _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
    }

    public IMongoCollection<BsonDocument> Collection(Type type)
    {
        var profile = _container.GetProfile(type);
        return _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);
    }

    public T Find<T>(ObjectId id)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name);
        var filter = Builders<T>.Filter.Eq("_id", id);
        return collection.FindSync(filter).FirstOrDefault();
    }

    public async Task<T> FindAsync<T>(ObjectId id, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        var filter = Builders<T>.Filter.Eq("_id", id);
        var query = await collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IAsyncCursor<T>> FindAsync<T>(Expression<Func<T, bool>> filter, FindOptions<T> options, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        var query = await collection.FindAsync(filter, cancellationToken: cancellationToken);
        return query;
    }

    public async Task<IAsyncCursor<T>> FindAsync<T>(FilterDefinition<T> filter, FindOptions<T> options, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        return await collection.FindAsync(filter, options: null, cancellationToken);
    }

    public IFindFluent<T, T> Find<T>(Expression<Func<T, bool>> filter)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        return collection.Find(filter);
    }

    public IFindFluent<T, T> Find<T>(FilterDefinition<T> filter)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        return collection.Find(filter);
    }

    public BsonDocument Find(string collectionName, ObjectId id)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName, _collectionSettings);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var query = collection.FindSync(filter);
        return query.FirstOrDefault();
    }

    public async Task<BsonDocument> FindAsync(string collectionName, ObjectId id)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName, _collectionSettings);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var query = await collection.FindAsync(filter);
        return await query.FirstOrDefaultAsync();
    }

    public BsonDocument Insert<T>(T model)
        where T : class
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);
        var document = model.ToBsonDocument();
        collection.InsertOne(document, new InsertOneOptions { BypassDocumentValidation = profile.BypassDocumentValidation });
        return document;
    }

    public async Task<BsonDocument> InsertAsync<T>(T model, CancellationToken cancellationToken = default)
        where T : class
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);
        var document = model.ToBsonDocument();
        await collection.InsertOneAsync(document, new InsertOneOptions { BypassDocumentValidation = profile.BypassDocumentValidation }, cancellationToken);
        return document;
    }

    public async Task InsertAsync<T>(IEnumerable<T> models, CancellationToken cancellationToken = default)
        where T : class
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);
        var documents = models.Select(model => model.ToBsonDocument());
        await collection.InsertManyAsync(documents, new InsertManyOptions { BypassDocumentValidation = profile.BypassDocumentValidation }, cancellationToken);
    }

    public BsonDocument Insert(string collectionName, BsonDocument document)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName, _collectionSettings);
        collection.InsertOne(document);
        return document;
    }

    public async Task<BsonDocument> InsertAsync(string collectionName, BsonDocument document, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName, _collectionSettings);
        await collection.InsertOneAsync(document, new InsertOneOptions { BypassDocumentValidation = true }, cancellationToken);
        return document;
    }

    public int Update<T>(ObjectId id, T model)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name);
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = collection.UpdateOne(filter, new BsonDocumentUpdateDefinition<T>(model.ToBsonDocument()));
        return (int)result.ModifiedCount;
    }

    public async Task<int> UpdateAsync<T>(ObjectId id, T model, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await collection.UpdateOneAsync(filter, new BsonDocument("$set", model.ToBsonDocument()), cancellationToken: cancellationToken);
        return (int)result.ModifiedCount;
    }

    public int Update(string collectionName, ObjectId id, BsonDocument document)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var result = collection.UpdateOne(filter, new BsonDocument("$set", document));
        return (int)result.ModifiedCount;
    }

    public async Task<int> UpdateAsync(string collectionName, ObjectId id, BsonDocument document, CancellationToken cancellationToken = default)
    {
        var collection = _database.GetCollection<BsonDocument>(collectionName, _collectionSettings);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var result = await collection.UpdateOneAsync(filter, new BsonDocument("$set", document), cancellationToken: cancellationToken);
        return (int)result.ModifiedCount;
    }

    public async Task<int> UpdateAsync<T>(IEnumerable<T> models, Func<T, object> keyGetter, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);

        var count = 0;
        foreach (var model in models)
        {
            var id = keyGetter(model);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var result = await collection.UpdateOneAsync(filter, new BsonDocument("$set", model.ToBsonDocument()), cancellationToken: cancellationToken);
            count += (int)result.ModifiedCount;
        }

        return count;
    }

    public async Task<int> DeleteAsync<T>(ObjectId id, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<BsonDocument>(profile.CollectionName ?? type.Name, _collectionSettings);
        var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
        var result = await collection.DeleteOneAsync(filter: filter, cancellationToken: cancellationToken);
        return (int)result.DeletedCount;
    }

    public async Task<int> DeleteAsync<T>(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        var type = typeof(T);
        var profile = _container.GetProfile<T>();
        var collection = _database.GetCollection<T>(profile.CollectionName ?? type.Name, _collectionSettings);
        var result = await collection.DeleteManyAsync(filter, null, cancellationToken);
        return (int)result.DeletedCount;
    }
}