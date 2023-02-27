namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class RolePermissionConfig:IEntityTypeConfiguration<RolePermissionMap>
{

    public void Configure(EntityTypeBuilder<RolePermissionMap> builder)
    {
        builder
            .HasKey(ur => new { ur.PermissionId, ur.RoleId });

        builder
            .HasOne(ur => ur.Permission)
            .WithMany(u => u.RolePermissionMap)
            .HasForeignKey(ur => ur.PermissionId)
            .IsRequired();

        builder
            .HasOne(ur => ur.Role)
            .WithMany(u => u.RolePermissionMaps)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

    }
}