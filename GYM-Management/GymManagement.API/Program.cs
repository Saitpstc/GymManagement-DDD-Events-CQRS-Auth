using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using Authorization_Authentication;
using Authorization_Authentication.Middlewares;
using Customer.Application.Contracts;
using Customer.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Shared.Application;
using Shared.Core;
using Shared.Infrastructure;
using Shared.Presentation;
using Shared.Presentation.Middlewares;


var builder = WebApplication.CreateBuilder(args);



builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(builder.Configuration);
});


var myOptions = new AppOptions();
builder.Services.AddPresentationDependency(builder.Configuration, myOptions);
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

/*builder.Services.AddAuthentication(x =>
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(myOptions.AuthTokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});*/
builder.Services.AddAuthorization();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<CustomExceptionHandler>();
app.UseAuthorization();


//app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.Run();