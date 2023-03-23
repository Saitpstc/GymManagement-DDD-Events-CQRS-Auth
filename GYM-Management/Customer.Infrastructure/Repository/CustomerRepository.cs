namespace Customer.Infrastructure.Repository;

using Core;
using Database;
using Database.Tables;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository:ICustomerRepository
{
    private readonly CustomerDbContext _dbContext;
    public CustomerDB TrackedModel { get; set; }

    public CustomerRepository(CustomerDbContext dbContext)
    {
        _dbContext = dbContext;

    }


    public async Task<Customer?> RetriveByAsync(Guid Id)
    {
        CustomerDB? databaseRecord = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == Id);


        if (databaseRecord is null)
        {
            return null;
        }

        Customer customer = databaseRecord.FromEntity();
        TrackedModel = databaseRecord;

        return customer;
    }



    public async Task<bool> UpdateAsync(Customer Aggregate)
    {

        
        try
        {
            _dbContext.Update(CustomerDB.FromDomainModel(Aggregate));
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while updating  to the database.", e);
        }

        return true;
    }

    public async Task DeleteByAsync(Customer Aggregate)
    {
        CustomerDB dbTable = CustomerDB.FromDomainModel(Aggregate);
        dbTable.IsDeleted = true;

        try
        {
            _dbContext.Update(dbTable);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while updating  to the database.", e);
        }


    }


    public async Task<Customer> AddAsync(Customer Aggregate)
    {
        CustomerDB dbTable = CustomerDB.FromDomainModel(Aggregate);

        try
        {
            await _dbContext.Customers.AddAsync(dbTable);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while adding the entity to the database.", e);
        }


        return dbTable.FromEntity();
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
            result = await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occurred while saving changes in the  database.", e);
        }

        return result;
    }
}