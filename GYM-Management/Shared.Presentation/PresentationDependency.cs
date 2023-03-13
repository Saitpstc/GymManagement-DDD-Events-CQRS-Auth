namespace Shared.Presentation;

using Core;
using FluentValidation.AspNetCore;
using Infrastructure.Email;
using Infrastructure.Email.EmailConfirmation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


public static class PresentationDependency
{
    public static void AddPresentationDependency(this IServiceCollection service, IConfiguration configuration, AppOptions myOptions)
    {


        configuration.Bind("AppOptions", myOptions);
        service.AddSingleton(myOptions);

        service.AddScoped<IEmailConfirmationService, LinkConfirmation>();
            service.AddScoped<IEmailConfirmationService, CodeConfirmation>();

        
        service.AddTransient<ConfirmationServiceResolver>(serviceProvider => token =>
        {
            // hardcoded strings can be extracted as constants
            return token switch
            {
                "Code" => serviceProvider.GetService<CodeConfirmation>(),
                "Link" => serviceProvider.GetService<LinkConfirmation>(),
                _ => throw new InvalidOperationException()
            };
        });
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