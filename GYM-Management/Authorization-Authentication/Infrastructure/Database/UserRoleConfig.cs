namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class UserRoleConfig:IEntityTypeConfiguration<UserRoleMap>
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

    }
}