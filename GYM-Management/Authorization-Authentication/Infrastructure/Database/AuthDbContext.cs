namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

public class AuthDbContext:IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRoleMap, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
{

    public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
    {

    }

    override protected void OnModelCreating(ModelBuilder builder)
    {
  
        base.OnModelCreating(builder);
        builder.Entity<UserRoleMap>()
               .HasKey(ur => new { ur.UserId, ur.RoleId });
c
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
    }
}