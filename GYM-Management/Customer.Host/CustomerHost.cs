﻿namespace Customer.Host;

using Application;
using Core;
using Customer.Application.Contracts;
using Customer.Infrastructure.Database;
using Customer.Infrastructure.Repository;
using GymManagement.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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



        services.AddScoped<ICustomerRepository, CustomerRepository>();


    }
}