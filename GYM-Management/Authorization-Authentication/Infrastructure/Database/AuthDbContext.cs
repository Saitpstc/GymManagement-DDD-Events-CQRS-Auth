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
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}