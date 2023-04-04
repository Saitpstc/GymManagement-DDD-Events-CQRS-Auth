namespace Shared.Infrastructure.Database;

using System.ComponentModel.DataAnnotations.Schema;
using Core.Domain;

public class DataStructureBase:BaseEntity
{
    [NotMapped]
    public List<IntegrationEvent> Events = new List<IntegrationEvent>();
    public bool IsDeleted { get; set; }
}