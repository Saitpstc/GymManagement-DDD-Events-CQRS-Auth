namespace Customer.Infrastructure.Database.Tables;

using Core;
using Core.Enums;
using Core.ValueObjects;
using Shared.Infrastructure;
using Shared.Infrastructure.Database;

public class CustomerDB:DataStructureBase
{

    public EmailDb EmailDb { get; set; }
    //  public Guid EmailId { get; set; }
    public MembershipDb MembershipDb { get; set; }
    public Guid? MembershipDbId { get; set; }
    public NameDb NameDb { get; set; }
    //  public Guid NameId { get; set; }
    public PhoneNumberDb NumberDb { get; set; }

    //   public Guid NumberId { get; set; }


    public Customer FromEntity()
    {


        var customer = new Customer(new Name(NameDb.FirstName, NameDb.LastName),
            new PhoneNumber(NumberDb.CountryCode, NumberDb.Number), new Email(EmailDb.email));

        if (MembershipDbId is not null)
        {
            customer.Membership = Membership.Custom(MembershipDb.StartDate, MembershipDb.EndDate, Id);
        }
        else
        {
            customer = new Customer(new Name(NameDb.FirstName, NameDb.LastName),
                new PhoneNumber(NumberDb.CountryCode, NumberDb.Number), new Email(EmailDb.email));
        }

        customer.Id = Id;
        return customer;

    }

    public static CustomerDB FromDomainModel(Customer Aggregate)
    {
        CustomerDB customerDb = new CustomerDB
        {
            EmailDb = new EmailDb
            {
                email = Aggregate.Email.ToString()
            },
            NameDb = new NameDb
            {
                FirstName = Aggregate.Name.NameOnly(),
                LastName = Aggregate.Name.SurNameOnly()
            },
            NumberDb = new PhoneNumberDb
            {
                Number = Aggregate.PhoneNumber.Number(),
                CountryCode = Aggregate.PhoneNumber.CountryCode()
            },
            Id = Aggregate.Id,
            LastUpdateAt = Aggregate.LastUpdateAt
        };

        if (Aggregate.Membership is not null)
        {
            customerDb.MembershipDb = new MembershipDb
            {
                EndDate = Aggregate.Membership.EndDate,
                StartDate = Aggregate.Membership.StartDate,
                Id = Aggregate.Membership.Id
            };
            return customerDb;
        }
        throw new NotImplementedException();
    }
}