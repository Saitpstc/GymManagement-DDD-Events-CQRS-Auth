using System.Diagnostics;
using System.Text.Json.Serialization;
using Authorization_Authentication;
using Authorization_Authentication.Middlewares;
using Customer.Application.Contracts;
using Customer.Infrastructure;
using GymManagement.API.Middlewares;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Application;
using Shared.Core;
using Shared.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

/*
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .Build();
       */


builder.Host.UseSerilog((context, configuration) =>
{

    configuration.ReadFrom.Configuration(builder.Configuration);



});

// Add services to the container.

var myOptions = new AppOptions();
builder.Configuration.Bind("AppOptions", myOptions);
builder.Services.AddSingleton(myOptions);

builder.Services.CustomerDependency(builder.Configuration, myOptions);
builder.Services.AddSharedDependency();
builder.Services.AddAuthDependency(myOptions);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.DefaultIgnoreCondition
        = JsonIgnoreCondition.WhenWritingNull);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestPipeline<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ICustomerModule, CustomerModule>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]
            {
            }
        }
    });
});




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthorization();
app.UseMiddleware<CustomExceptionHandler>();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.Run();