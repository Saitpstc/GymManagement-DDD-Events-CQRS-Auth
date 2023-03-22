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
        services.AddDbContext<CustomerDbContext>(options =>
        {
            options.UseSqlServer(appOptions.GetConnectionString(Modules.Customer));
        });

        services.AddValidatorsFromAssembly(typeof(ICustomerModule).Assembly);

        services.AddScoped<ICustomerRepository, CustomerRepository>();




    }
}