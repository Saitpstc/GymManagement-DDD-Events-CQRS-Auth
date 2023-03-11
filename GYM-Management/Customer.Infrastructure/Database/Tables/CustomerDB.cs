namespace Customer.Infrastructure.Database.Tables;

using Core;
using Core.Enums;
using Core.ValueObjects;
using Shared.Infrastructure;

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
        Customer customer;

        if (MembershipDbId is not null)
        {
            if (MembershipDb.SubscriptionType == SubscriptionEnum.Custom)
            {
                customer = new Customer(new Name(NameDb.FirstName, NameDb.LastName),
                    new PhoneNumber(NumberDb.CountryCode, NumberDb.Number), new Email(EmailDb.email),
                    Membership.Custom(MembershipDb.StartDate, MembershipDb.EndDate, Id));

                customer.Id = Id;
                return customer;

            }
            customer = new Customer(new Name(NameDb.FirstName, NameDb.LastName),
                new PhoneNumber(NumberDb.CountryCode, NumberDb.Number), new Email(EmailDb.email),
                Membership.Custom(MembershipDb.StartDate, MembershipDb.EndDate, Id));

            customer.Id = Id;
            return customer;
        }
        customer = new Customer(new Name(NameDb.FirstName, NameDb.LastName),
            new PhoneNumber(NumberDb.CountryCode, NumberDb.Number), new Email(EmailDb.email));
        customer.Id = Id;
        return customer;

    }

    public static CustomerDB FromDomainModel(Customer Aggregate)
    {
        CustomerDB customerDb = new CustomerDB
        {
            EmailDb = new EmailDb
            {
                email = Aggregate.GetMail().ToString()
            },
            NameDb = new NameDb
            {
                FirstName = Aggregate.GetName().NameOnly(),
                LastName = Aggregate.GetName().SurNameOnly()
            },
            NumberDb = new PhoneNumberDb
            {
                Number = Aggregate.GetNumber().Number(),
                CountryCode = Aggregate.GetNumber().CountryCode()
            },
            Id = Aggregate.Id,
            LastUpdateAt = Aggregate.LastUpdateAt
        };

        if (Aggregate.GetMembership() is not null)
        {
            customerDb.MembershipDb = new MembershipDb
            {
                EndDate = Aggregate.GetMembership().EndsAt(),
                StartDate = Aggregate.GetMembership().StartedAt(),
                SubscriptionType = Aggregate.GetMembership().SubscriptionType()
            };
            customerDb.MembershipDbId = Aggregate.GetMembership().Id;
        }
        return customerDb;
    }
}