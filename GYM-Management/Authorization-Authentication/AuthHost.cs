namespace Authorization_Authentication;

using GymManagement.API.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    }
}