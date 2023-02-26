namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class AuthDbContext:IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRoleMap, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermissionMap> RolePermissionMaps { get; set; }

    public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
    {

    }

    override protected void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        builder.Entity<UserRoleMap>()
               .HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<UserRoleMap>()
               .HasOne(ur => ur.Role)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

        builder.Entity<UserRoleMap>()
               .HasOne(ur => ur.User)
               .WithMany(u => u.UserRoles)
               .HasForeignKey(ur => ur.UserId)
               .IsRequired();

        builder.Entity<RolePermissionMap>()
               .HasKey(ur => new { ur.PermissionId, ur.RoleId });

        builder.Entity<RolePermissionMap>()
               .HasOne(ur => ur.Permission)
               .WithMany(u => u.RolePermissionMap)
               .HasForeignKey(ur => ur.PermissionId)
               .IsRequired();

        builder.Entity<RolePermissionMap>()
               .HasOne(ur => ur.Role)
               .WithMany(u => u.RolePermissionMaps)
               .HasForeignKey(ur => ur.Role)
               .IsRequired();
    }
}