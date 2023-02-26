namespace Authorization_Authentication;

using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;

public static class AuthHost
{

    public static void AddAuthDependency(this IServiceCollection service)
    {
        // Register DbContext
        service.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer("Server=213.142.151.220;Database=GYM.Auth;User ID=server1;Password=Sait.Bozzisha.2248;Trust Server Certificate=true"));

        // Register Identity services
        service.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
    }
}