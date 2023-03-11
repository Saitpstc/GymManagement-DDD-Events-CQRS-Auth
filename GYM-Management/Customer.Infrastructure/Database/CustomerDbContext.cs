namespace Customer.Infrastructure.Database;

using Configuration;
using Microsoft.EntityFrameworkCore;
using Tables;

public class CustomerDbContext:DbContext, ICustomerDbContext
{

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options):base(options)
    {

    }

    public DbSet<CustomerDB> Customers { get; set; }
    public DbSet<MembershipDb> Membership { get; set; }



    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}