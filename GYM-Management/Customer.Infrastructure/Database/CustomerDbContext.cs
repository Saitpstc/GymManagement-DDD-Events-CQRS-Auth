namespace Customer.Infrastructure.Database;

using Configuration;
using Core;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;


public class CustomerDbContext:AppDbContext
{


    public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediator mediator):base(options, mediator)
    {

    }

    public DbSet<Customer> Customers { get; set; }



    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}