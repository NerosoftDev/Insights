namespace Nerosoft.Insights.Framework;

public interface IEntity
{
}

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }

    DateTime CreateTime { get; set; }
}