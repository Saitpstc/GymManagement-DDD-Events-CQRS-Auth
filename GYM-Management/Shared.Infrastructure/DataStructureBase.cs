namespace Shared.Infrastructure;

using System.ComponentModel.DataAnnotations.Schema;
using Core;

public class DataStructureBase:BaseEntity
{
    [NotMapped]
    public List<DomainEvent> Events = new List<DomainEvent>();
    public bool IsDeleted { get; set; }
}