namespace Shared.Infrastructure.Database;

using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

public class DataStructureBase:BaseEntity
{
    [NotMapped]
    public List<DomainEvent> Events = new List<DomainEvent>();
    public bool IsDeleted { get; set; }
}