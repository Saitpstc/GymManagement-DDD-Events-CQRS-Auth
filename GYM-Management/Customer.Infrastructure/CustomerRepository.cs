namespace Customer.Infrastructure;

using Core;
using Shared.Core;
using Shared.Infrastructure;

public class CustomerRepository:DataStructureMap<Customer>,ICustomerRepository
{
    public Task<Customer> RetriveBy(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task Update(AggregateRoot Aggregate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBy(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task Add(AggregateRoot Aggregate)
    {
        throw new NotImplementedException();
    }

    protected override Customer MapToAggregate(DataStructureBase DatabaseTable)
    {
        throw new NotImplementedException();
    }

    protected override DataStructureBase MapToDatabaseTable(Customer Aggregate)
    {
        throw new NotImplementedException();
    }
}