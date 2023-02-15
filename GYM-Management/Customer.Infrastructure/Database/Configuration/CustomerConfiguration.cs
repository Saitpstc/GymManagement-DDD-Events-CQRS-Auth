namespace Customer.Infrastructure.Database.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tables;

internal class CustomerConfiguration:IEntityTypeConfiguration<CustomerDB>
{

    void IEntityTypeConfiguration<CustomerDB>.Configure(EntityTypeBuilder<CustomerDB> builder)
    {
        builder.OwnsOne(customer => customer.MembershipDb);
        builder.OwnsOne(customer => customer.NameDb);
        builder.OwnsOne(customer => customer.NumberDb);
        builder.OwnsOne(customer => customer.EmailDb);
        builder.HasKey(customer => customer.Id);
        



    }
}