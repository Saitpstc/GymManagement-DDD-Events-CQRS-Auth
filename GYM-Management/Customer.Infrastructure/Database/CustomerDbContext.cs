namespace Customer.Infrastructure.Database;

using System.Reflection;
using Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Tables;

public class CustomerDbContext:DbContext, ICustomerDbContext
{

    public DbSet<CustomerDB> Customers { get; set; }
    public DbSet<MembershipDb> Membership { get; set; }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options):base(options)
    {

    }



    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}