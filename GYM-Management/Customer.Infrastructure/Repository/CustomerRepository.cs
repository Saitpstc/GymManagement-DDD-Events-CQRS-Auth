namespace Customer.Infrastructure.Repository;

using Core;
using Core.ValueObjects;
using Database;
using Database.Tables;
using Shared.Core;
using Shared.Infrastructure;

public class CustomerRepository:ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;

    }


    public async Task<Customer?> RetriveByAsync(Guid Id)
    {
        CustomerDB? databaseRecord = await _dbContext.Customers.FindAsync(Id);

        if (databaseRecord is null)
        {
            return null;
        }

        Customer customer = databaseRecord.FromEntity();


        return customer;
    }

    public async Task<bool> UpdateAsync(Customer Aggregate)
    {
        _dbContext.Update(CustomerDB.FromDomainModel(Aggregate));
        var result = await _dbContext.CommitAsync();
        return result != 0;
    }

    public async Task<bool> DeleteByAsync(Customer Aggregate)
    {
        var dbTable = CustomerDB.FromDomainModel(Aggregate);
        dbTable.IsDeleted = true;
        _dbContext.Update(dbTable);
        var result = await _dbContext.CommitAsync();
        return result != 0;
       
    }


    public async Task<Customer> AddAsync(Customer Aggregate)
    {
        var dbTable = CustomerDB.FromDomainModel(Aggregate);

        await _dbContext.Customers.AddAsync(dbTable);

        await _dbContext.CommitAsync();

        return dbTable.FromEntity();
    }

    public Task<IEnumerable<Customer>> GetAllAsync(Customer Aggregate)
    {
        throw new NotImplementedException();
    }





}