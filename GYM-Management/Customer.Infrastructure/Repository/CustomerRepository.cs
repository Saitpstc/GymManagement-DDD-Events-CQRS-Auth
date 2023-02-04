namespace Customer.Infrastructure.Repository;

using Core;
using Database;
using Shared.Core;

internal class CustomerRepository:ICustomerRepository
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

    public Task Add(Customer Aggregate)
    {
        throw new NotImplementedException();
    }
}