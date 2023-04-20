using Microsoft.EntityFrameworkCore;
using Nerosoft.Insights.Storage.Domain;

namespace Nerosoft.Insights.Storage.Domain;

public class EfCoreDataContext : DbContext
{
    public EfCoreDataContext(DbContextOptions<EfCoreDataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("session_log");

            entity.Property(p => p.CreateTime)
                  .ValueGeneratedOnAdd()
                  .HasValueGenerator<DateTimeGenerator>();
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("event_log");

            entity.Property(p => p.CreateTime)
                  .ValueGeneratedOnAdd()
                  .HasValueGenerator<DateTimeGenerator>();

            entity.HasMany(t => t.Properties)
                  .WithOne()
                  .HasForeignKey(t => t.LogId);
        });

        modelBuilder.Entity<Error>(entity =>
        {
            entity.ToTable("error_log");

            entity.Property(p => p.CreateTime)
                  .ValueGeneratedOnAdd()
                  .HasValueGenerator<DateTimeGenerator>();

            entity.HasMany(t => t.Properties)
                  .WithOne()
                  .HasForeignKey(t => t.LogId);

            entity.OwnsOne(t => t.Exception, builder =>
            {
                builder.ToJson();
            });

            entity.OwnsMany(t => t.Threads, builder =>
            {
                builder.ToJson();
            });
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.ToTable("log_property");

            entity.Property(p => p.Id)
                  .ValueGeneratedOnAdd()
                  .HasValueGenerator<GuidGenerator>();
        });
    }
}