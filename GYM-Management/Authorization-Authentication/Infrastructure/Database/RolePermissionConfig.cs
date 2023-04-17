namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

internal class RolePermissionConfig:IEntityTypeConfiguration<RolePermission>
{

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder
            .HasKey(ur => new { ur.PermissionType, ur.PermissionContext, ur.RoleId });



        builder
            .HasOne(ur => ur.Role)
            .WithMany(u => u.RolePermissionMaps)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

    }
}