namespace Customer.Infrastructure.Database.Tables;

using Shared.Core;

public class Name:BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}