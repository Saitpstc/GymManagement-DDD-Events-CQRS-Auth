namespace Ledger.Infrastructure;

using Core;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core;

public static class LedgerDependencies
{
    public static void AddLedgerDependency(this IServiceCollection service, AppOptions appOptions)
    {
       // service.AddScoped<IStripeService, StripeService>();
        //service.AddScoped<IPaymentService, CreditCardPaymentService>();

    }
}