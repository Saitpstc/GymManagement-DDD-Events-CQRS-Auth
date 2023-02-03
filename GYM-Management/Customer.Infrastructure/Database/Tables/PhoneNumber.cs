namespace Customer.Infrastructure.Database.Tables;

using Shared.Core;

public class PhoneNumber:BaseEntity
{
    public string Number { get; set; }
    public string CountryCode { get; set; }
}