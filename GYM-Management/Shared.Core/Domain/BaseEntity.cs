namespace Shared.Core.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public DateTime LastUpdateAt { get; set; }
}