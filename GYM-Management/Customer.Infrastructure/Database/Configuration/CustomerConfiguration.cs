namespace Customer.Infrastructure.Database.Configuration;

using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class CustomerConfiguration:IEntityTypeConfiguration<Customer>
{
    void IEntityTypeConfiguration<Customer>.Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.OwnsOne(customer => customer.Name);
        builder.OwnsOne(customer => customer.PhoneNumber);
        builder.OwnsOne(customer => customer.Email);
        builder.OwnsOne(customer => customer.Membership, navigationBuilder =>
        {
            navigationBuilder.OwnsOne(x => x.Status);
            navigationBuilder.ToTable("Membership");
            navigationBuilder.WithOwner().HasForeignKey("CustomerId");
        });
        builder.OwnsMany(customer => customer.Bills, navigationBuilder =>
        {
            navigationBuilder.ToTable("CustomerInvoices");
            navigationBuilder.Property(x => x.Value).HasColumnName("InvoiceId");
            navigationBuilder.WithOwner().HasForeignKey("CustomerId");
            navigationBuilder.HasKey("Id");

        });
        builder.HasKey(customer => customer.Id);
    }
}