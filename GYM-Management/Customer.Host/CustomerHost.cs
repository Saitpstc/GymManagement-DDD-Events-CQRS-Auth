﻿namespace Customer.Host;

using Core;
using Customer.Application.Contracts;
using Customer.Infrastructure.Database;
using Customer.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CustomerHost
{
    public static void CustomerDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>(builder
            => builder.UseSqlServer("Server=213.142.151.220;Database=GYM.Customer;User ID=server1;Password=Sait.Bozzisha.2248;"));

        services.AddMediatR(typeof(ICustomerModule).Assembly);
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}