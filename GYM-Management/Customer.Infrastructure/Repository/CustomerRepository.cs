namespace Customer.Infrastructure.Repository;

using Core;
using Core.ValueObjects;
using Database;
using Database.Tables;
using Shared.Core;
using Shared.Infrastructure;

internal class CustomerRepository:DataStructureMap<CustomerDB, Customer>, ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;

    }


    public Task<Customer> RetriveBy(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer Aggregate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBy(Guid Id)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> Add(Customer Aggregate)
    {
        var dbTable = MapToDatabaseTable(Aggregate);
        
        await _dbContext.Customers.AddAsync(dbTable);

        await _dbContext.CommitAsync();

        return MapToAggregate(dbTable);
    }




    public Customer MapToAggregate(CustomerDB DatabaseTable)
    {
        return new Customer(new Name(DatabaseTable.NameDb.FirstName, DatabaseTable.NameDb.LastName),
            new PhoneNumber(DatabaseTable.NumberDb.CountryCode, DatabaseTable.NumberDb.Number), new Email(DatabaseTable.EmailDb.email));
    }

    public CustomerDB MapToDatabaseTable(Customer Aggregate)
    {
        return new CustomerDB()
        {
            EmailDb = new EmailDb()
            {
                email = Aggregate.GetMail().ToString()
            },
            MembershipDb = new MembershipDb()
            {
                EndDate = Aggregate.GetMembership().EndsAt(),
                StartDate = Aggregate.GetMembership().StartedAt(),
                SubscriptionType = Aggregate.GetMembership().SubscriptionType()
            },
            NameDb = new NameDb()
            {
                FirstName = Aggregate.GetName().NameOnly(),
                LastName = Aggregate.GetName().SurNameOnly()
            },
            NumberDb = new PhoneNumberDb()
            {
                Number = Aggregate.GetNumber().Number(),
                CountryCode = Aggregate.GetNumber().CountryCode()
            }
        };
    }
}