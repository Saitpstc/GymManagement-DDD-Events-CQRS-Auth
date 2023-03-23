namespace Customer.Infrastructure.Database.Configuration;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class CustomerConfiguration:IEntityTypeConfiguration<Customer>
{

    void IEntityTypeConfiguration<Customer>.Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(customer => customer.Name);
        builder.OwnsOne(customer => customer.PhoneNumber);
        builder.OwnsOne(customer => customer.Email);
        builder.OwnsOne(customer => customer.Membership);
        builder.HasKey(customer => customer.Id);



    }
}