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
        builder.HasKey(customer => customer.Id);

        builder.HasOne(x => x.Membership).WithOne(x => x.Customer).HasForeignKey<Customer>(x => x.MembershipId).OnDelete(DeleteBehavior.SetNull);
    }
}