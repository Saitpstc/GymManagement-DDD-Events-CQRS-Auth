namespace Authorization_Authentication;

using System.Reflection;
using System.Text;
using Application.Contracts;
using FluentValidation;
using Infrastructure;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Models;
using Shared.Core;
using Shared.Infrastructure;

public static class AuthHost
{

    public static void AddAuthDependency(this IServiceCollection service, AppOptions appOptions, IWebHostEnvironment webHostEnvironment)
    {

        var hosten = webHostEnvironment.EnvironmentName;

        // Register DbContext
        if (hosten.Equals("Testing"))
        {
            service.AddDbContext<AuthDbContext>(options =>
                options.UseInMemoryDatabase("TestingAuth"));
        }
        else
        {
            service.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(appOptions.GetConnectionString(Modules.Auth)));

        }

        // Register Identity services
        service.AddIdentity<User, Role>()
               .AddEntityFrameworkStores<AuthDbContext>()
               .AddDefaultTokenProviders();
        service.Configure<IdentityOptions>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;
        });

        service.AddHttpContextAccessor();
        // service.AddScoped<IAuthService,AuthService>();





        service.AddValidatorsFromAssembly(typeof(IAuthModule).Assembly);
        service.AddMediatR(typeof(IAuthModule).Assembly);
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



        service.AddScoped<IAuthModule, AuthModule>();

    }
}