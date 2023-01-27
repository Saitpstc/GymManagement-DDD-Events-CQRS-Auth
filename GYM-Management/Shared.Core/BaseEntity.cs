namespace Shared.Core;

public abstract class BaseEntity
{
    public Guid Id { get; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime LastUpdateAt { get; set; }
}