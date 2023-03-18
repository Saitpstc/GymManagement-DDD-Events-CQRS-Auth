namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{

    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new List<Role>()
        {
            new Role()
            {
                Id = new Guid("C28EFD96-582E-4855-9822-5CFE4D988543"),
                IsActive = true,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN"

            }
        });
    }
}