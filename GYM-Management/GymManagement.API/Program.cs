using System.Diagnostics;
using Customer.Application.Contracts;
using Customer.Host;
using Customer.Infrastructure;
using GymManagement.API.Controllers.Customer;
using Serilog;
using Shared.Application;


var builder = WebApplication.CreateBuilder(args);

/*builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();


builder.Host.UseSerilog((context, configuration) => configuration.WriteTo.Console().ReadFrom.Configuration(builder.Configuration));*/

// Add services to the container.
builder.Services.CustomerDependency(builder.Configuration);
builder.Services.AddSharedDependency();

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