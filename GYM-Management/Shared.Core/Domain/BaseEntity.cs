namespace Shared.Core.Domain;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime LastUpdateAt { get; set; }

    public bool IsDeleted { get; set; }
}