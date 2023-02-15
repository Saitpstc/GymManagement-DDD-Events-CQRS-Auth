namespace Customer.Infrastructure.Database;

using System.Reflection;
using Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure;
using Tables;

internal class CustomerDbContext:AppDbContext
{

    public DbSet<CustomerDB> Customers { get; set; }

    public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediator mediator):base(mediator, options)
    {

    }

    

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}