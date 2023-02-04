namespace Customer.Infrastructure.Database.Tables;

using System.ComponentModel.DataAnnotations.Schema;
using Shared.Core;
using Shared.Infrastructure;

internal class CustomerDB:DataStructureBase
{

    public EmailDb EmailDb { get; set; }
  //  public Guid EmailId { get; set; }
    public MembershipDb MembershipDb { get; set; }
  //  public Guid MembershipId { get; set; }
    public NameDb NameDb { get; set; }
  //  public Guid NameId { get; set; }
    public PhoneNumberDb NumberDb { get; set; }
 //   public Guid NumberId { get; set; }
    public int TotalMonthsOfMembership { get; set; }
}