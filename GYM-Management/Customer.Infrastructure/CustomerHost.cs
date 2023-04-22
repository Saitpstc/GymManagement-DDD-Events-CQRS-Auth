namespace Customer.Infrastructure;

using Application.Contracts;
using Core;
using Database;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Shared.Core;

public static class CustomerHost
{
    public static void CustomerDependency(this IServiceCollection services, IConfiguration configuration, AppOptions appOptions)
    {

        services.AddMediatR(typeof(ICustomerModule).Assembly);
        var hosten = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Register DbContext
        if (hosten.Equals("Testing"))
        {
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseInMemoryDatabase("TestingAuth"));
        }
        else
        {
            services.AddDbContext<CustomerDbContext>(options =>
                options.UseSqlServer(appOptions.GetConnectionString(Modules.Auth)));

        }

        services.AddValidatorsFromAssembly(typeof(ICustomerModule).Assembly);

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerModule, CustomerModule>();



    }
}