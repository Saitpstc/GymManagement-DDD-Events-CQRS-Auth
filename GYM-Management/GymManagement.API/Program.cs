using System.Diagnostics;
using Authorization_Authentication;
using Customer.Application.Contracts;
using Customer.Host;
using Customer.Infrastructure;
using GymManagement.API.Controllers.Customer;
using GymManagement.API.Models;
using Microsoft.Extensions.Options;
using Serilog;
using Shared.Application;


var builder = WebApplication.CreateBuilder(args);

/*builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();


builder.Host.UseSerilog((context, configuration) => configuration.WriteTo.Console().ReadFrom.Configuration(builder.Configuration));*/

// Add services to the container.

var myOptions = new AppOptions();
builder.Configuration.Bind("AppOptions", myOptions);
builder.Services.AddSingleton(myOptions);

builder.Services.CustomerDependency(builder.Configuration, myOptions);
builder.Services.AddSharedDependency();
builder.Services.AddAuthDependency(myOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerModule, CustomerModule>();

builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();