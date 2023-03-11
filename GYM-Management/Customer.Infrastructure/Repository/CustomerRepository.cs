namespace Customer.Infrastructure.Repository;

using Core;
using Database;
using Database.Tables;

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

        return true;
    }

    public async Task DeleteByAsync(Customer Aggregate)
    {
        CustomerDB dbTable = CustomerDB.FromDomainModel(Aggregate);
        dbTable.IsDeleted = true;
        _dbContext.Update(dbTable);



    }


    public async Task<Customer> AddAsync(Customer Aggregate)
    {
        CustomerDB dbTable = CustomerDB.FromDomainModel(Aggregate);

        await _dbContext.Customers.AddAsync(dbTable);



        return dbTable.FromEntity();
    }

    public Task<IEnumerable<Customer>> GetAllAsync(Customer Aggregate)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CommitAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}