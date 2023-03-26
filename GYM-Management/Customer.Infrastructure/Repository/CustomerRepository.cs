namespace Customer.Infrastructure.Repository;

using Core;
using Database;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository:ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;


    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;

    }


    public async Task<Customer?> RetriveByAsync(Guid Id)
    {
        var databaseRecord = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == Id);


        if (databaseRecord is null)
        {
            return null;
        }


        return databaseRecord;
    }



    public Task<bool> UpdateAsync(Customer Aggregate)
    {

        try
        {
            _dbContext.Update(Aggregate);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while updating  to the database.", e);
        }

        return Task.FromResult(true);
    }

    public Task DeleteByAsync(Customer Aggregate)
    {

        Aggregate.IsDeleted = true;

        try
        {
            _dbContext.Update(Aggregate);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while updating  to the database.", e);
        }
        return Task.CompletedTask;


    }


    public async Task<Customer> AddAsync(Customer Aggregate)
    {

        try
        {
            await _dbContext.Customers.AddAsync(Aggregate);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while adding the entity to the database.", e);
        }


        return Aggregate;
    }

    public Task<IEnumerable<Customer>> GetAllAsync(Customer Aggregate)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CommitAsync()
    {
        int result = 0;

        try
        {
            result = await _dbContext.SaveAsync();
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while saving changes in the  database.", e);
        }

        return result;
    }
}