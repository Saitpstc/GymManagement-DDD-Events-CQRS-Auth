namespace Customer.Infrastructure.Database;

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

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=213.142.151.220;Database=GYM.Customer;User ID=server1;Password=Sait.Bozzisha.2248;");
        base.OnConfiguring(optionsBuilder);
        
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}