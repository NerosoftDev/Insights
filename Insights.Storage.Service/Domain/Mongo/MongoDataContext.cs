using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using Nerosoft.Insights.Framework;
using Nerosoft.Insights.Storage.Domain;

namespace Nerosoft.Insights.Storage.Domain;

public class MongoDataContext : MongoDbContext
{
    public MongoDataContext(IOptions<MongoDbOptions> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //BsonClassMap.RegisterClassMap<Log>(map =>
        //{
        //    map.SetIsRootClass(true);
        //    map.AutoMap();
        //    map.AddKnownType(typeof(Event));
        //    map.AddKnownType(typeof(Page));
        //    map.AddKnownType(typeof(Error));
        //});

        builder.For<Session>(profile =>
        {
            profile.ToCollection("sessions");
            profile.BypassValidation(true);
        });

        builder.For<Event>(profile =>
        {
            profile.ToCollection("events");
            profile.BypassValidation(true);
        });

        builder.For<Error>(profile =>
        {
            profile.ToCollection("errors");
            profile.BypassValidation(true);
        });

        builder.For<Page>(profile =>
        {
            profile.ToCollection("pages");
            profile.BypassValidation(true);
        });
    }
}