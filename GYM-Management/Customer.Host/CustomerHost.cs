﻿namespace Customer.Host;

using Core;
using Customer.Application.Contracts;
using Customer.Infrastructure.Database;
using Customer.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CustomerHost
{
    public static void CustomerDependency(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerDbContext>();

        services.AddMediatR(typeof(ICustomerModule).Assembly);
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
}