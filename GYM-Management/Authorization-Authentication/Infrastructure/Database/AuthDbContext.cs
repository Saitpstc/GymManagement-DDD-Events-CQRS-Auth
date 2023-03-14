﻿namespace Authorization_Authentication.Infrastructure.Database;

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

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ConfirmationCode> ConfirmationCodes { get; set; }
    public DbSet<RolePermissionMap> RolePermissionMaps { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    override protected void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}