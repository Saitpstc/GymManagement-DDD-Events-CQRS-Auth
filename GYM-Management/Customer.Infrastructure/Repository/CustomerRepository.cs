
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
    
    public Task<AggregateRoot> RetriveBy(Guid Id)
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
}