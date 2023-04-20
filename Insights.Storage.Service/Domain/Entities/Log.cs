using Nerosoft.Insights.Framework;

namespace Nerosoft.Insights.Storage.Domain;

public abstract class Log : IEntity<Guid>
{
    public virtual Guid Id { get; set; }

    public virtual Guid AppId { get; set; }

    public virtual string AppSecret { get; set; }

    public virtual string InstallId { get; set; }

    public virtual DateTime? Timestamp { get; set; }

    public virtual string UserId { get; set; }

    public virtual List<Property> Properties { get; set; }

    public virtual DateTime CreateTime { get; set; }
}