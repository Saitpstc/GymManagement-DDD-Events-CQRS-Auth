﻿namespace Authorization_Authentication.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

class UserConfiguration:IEntityTypeConfiguration<User>
{

    public void Configure(EntityTypeBuilder<User> builder)
    {
        User user = new User
        {
            UserName = "SuperAdmin",
            Email = "saitpostaci8@gmail.com",
            Id = new Guid("F482BCCA-98DB-438B-906B-4860E14ADCCE"),
            EmailConfirmed = true,
            NormalizedUserName = "SUPERADMIN",
            PhoneNumberConfirmed = true,
            NormalizedEmail = "SAITPOSTACI8@GMAIL.COM",
            SecurityStamp = new Guid("6CD014E1-44A0-451A-95B4-0D76FD574A93").ToString(),
            ConcurrencyStamp = "d24fc809-1851-4c0b-bf8e-08c4c80ae5c5"



        };

        var hash = "AQAAAAEAACcQAAAAEJ2fKtOl0O+pP9HPy5C5s7azNMQiBw1wQedbGzbT8RPPDGazyiXXkXEj2Aa9xThYZQ==";


        user.PasswordHash = hash;
        builder.HasData(new List<User>
        {
            user
        });
    }
}