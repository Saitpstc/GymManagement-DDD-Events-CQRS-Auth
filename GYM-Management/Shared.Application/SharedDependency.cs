namespace Shared.Application;

using Contracts;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

public static class SharedDependency
{
    public static void AddSharedDependency(this IServiceCollection services)
    {
        services.AddScoped<IErrorMessageCollector, ErrorMessageCollector>();
        services.AddSingleton<ISerilogContext, SerilogContext>();
    }
}