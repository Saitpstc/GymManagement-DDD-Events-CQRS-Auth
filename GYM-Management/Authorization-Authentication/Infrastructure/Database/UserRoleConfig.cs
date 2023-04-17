namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class UserRoleConfig:IEntityTypeConfiguration<UserRoleMap>
{

    public void Configure(EntityTypeBuilder<UserRoleMap> builder)
    {
        builder
            .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        builder
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

        builder.HasData(new List<UserRoleMap>()
        {
            new UserRoleMap()
            {
                RoleId = new Guid("C28EFD96-582E-4855-9822-5CFE4D988543"),
                UserId = new Guid("F482BCCA-98DB-438B-906B-4860E14ADCCE")
            }
        });

    }
}