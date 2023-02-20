namespace Customer.Infrastructure.Database.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tables;

public class CustomerConfiguration:IEntityTypeConfiguration<CustomerDB>
{

    void IEntityTypeConfiguration<CustomerDB>.Configure(EntityTypeBuilder<CustomerDB> builder)
    {
        builder.OwnsOne(customer => customer.NameDb);
        builder.OwnsOne(customer => customer.NumberDb);
        builder.OwnsOne(customer => customer.EmailDb);
        builder.HasKey(customer => customer.Id);

        builder.HasOne(x => x.MembershipDb).WithOne(x => x.Customer).HasForeignKey<CustomerDB>(db => db.MembershipDbId).IsRequired(false);




    }
}