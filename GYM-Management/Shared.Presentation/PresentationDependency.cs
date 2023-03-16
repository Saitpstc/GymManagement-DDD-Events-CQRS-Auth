namespace Shared.Presentation;

using Core;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Mail;
using Infrastructure.Mail.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;

public static class PresentationDependency
{
    public static void AddPresentationDependency(this IServiceCollection service, IConfiguration configuration, AppOptions myOptions)
    {


        configuration.Bind("AppOptions", myOptions);
        service.AddSingleton(myOptions);




        service.AddSendGrid(options => options.ApiKey = myOptions.SendgridApi);
        service.AddScoped<IEmailService, EmailService>();
        service.AddScoped<IMailFactory, MailFactory>();

        service.AddFluentValidationAutoValidation();
        service.AddSwaggerGen(options =>
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

    }
}