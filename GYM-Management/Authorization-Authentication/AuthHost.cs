﻿namespace Authorization_Authentication;

using System.Text;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Models;
using Shared.Core;

public static class AuthHost
{

    public static void AddAuthDependency(this IServiceCollection service, AppOptions appOptions)
    {
        // Register DbContext
        service.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(appOptions.GetConnectionString(Modules.Auth)));

        // Register Identity services
        service.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<AuthDbContext>()
               .AddDefaultTokenProviders();
        
        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appOptions.AuthTokenKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    }
}