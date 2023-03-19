namespace Customer.Infrastructure.Database;

using Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Tables;

public class CustomerDbContext:AppDbContext
{


    public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediator mediator):base(options, mediator)
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