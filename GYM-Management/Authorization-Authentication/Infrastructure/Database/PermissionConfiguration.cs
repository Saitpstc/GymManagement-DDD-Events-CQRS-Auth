namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class PermissionConfiguration:IEntityTypeConfiguration<Permission>
{

    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        
    }
}