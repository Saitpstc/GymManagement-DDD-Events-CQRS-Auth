namespace Customer.Infrastructure.Database.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tables;

public class MembershipConfiguration:IEntityTypeConfiguration<MembershipDb>
{
    void IEntityTypeConfiguration<MembershipDb>.Configure(EntityTypeBuilder<MembershipDb> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Customer).WithOne(x => x.MembershipDb).HasForeignKey<MembershipDb>(x => x.CustomerId).IsRequired();
    }
}