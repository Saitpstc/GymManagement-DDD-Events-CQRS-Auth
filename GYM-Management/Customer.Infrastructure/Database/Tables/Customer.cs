namespace Customer.Infrastructure.Database.Tables;

using System.ComponentModel.DataAnnotations.Schema;
using Shared.Core;
using Shared.Infrastructure;

public class Customer:DataStructureBase
{

    public Email Email { get; set; }
    public Guid EmailId { get; set; }
    public Membership Membership { get; set; }
    public Guid MembershipId { get; set; }
    public Name Name { get; set; }
    public Guid NameId { get; set; }
    public PhoneNumber Number { get; set; }
    public Guid NumberId { get; set; }
    public int TotalMonthsOfMembership { get; set; }
}