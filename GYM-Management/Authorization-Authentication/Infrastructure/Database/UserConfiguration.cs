namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class UserConfiguration:IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user = new User()
        {
            UserName = "SuperAdmin",
            Email = "saitpostaci8@gmail.com",
            Id = new Guid("F482BCCA-98DB-438B-906B-4860E14ADCCE"),
            EmailConfirmed = true,
            NormalizedUserName = "SUPERADMIN",
            PhoneNumberConfirmed = true,
            NormalizedEmail = "SAITPOSTACI8@GMAIL.COM",
            SecurityStamp = new Guid("6CD014E1-44A0-451A-95B4-0D76FD574A93").ToString()
            


        };
        var passwordhasher = new PasswordHasher<User>();
        var hash = passwordhasher.HashPassword(user, "Sait.Modular.2248");
        
        
        user.PasswordHash = hash;
        builder.HasData(new List<User>()
        {
            user
        });
    }
}