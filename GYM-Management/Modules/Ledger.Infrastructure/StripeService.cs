namespace Ledger.Infrastructure;

using Auth.Entry;
using Core;
using IntegrationEvents.LedgerModule;
using MediatR;
using Shared.Core;
using Shared.Core.Exceptions;
using Stripe;
using Test;
using Invoice = Ledger.Core.Invoice;

public class StripeService:IStripeService
{
    private readonly AppOptions _appOptions;
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public StripeService(AppOptions appOptions, IAuthService authService, IMediator mediator)
    {
        _appOptions = appOptions;
        _authService = authService;
        _mediator = mediator;

    }

    public async Task<string> CreateCustomerAsync(string userEmail)
    {
        StripeConfiguration.ApiKey = _appOptions.StripeApi;

        var customerOptions = new CustomerCreateOptions
        {
            Email = userEmail,
        };

        var customerService = new CustomerService();
        var customer = await customerService.CreateAsync(customerOptions);

        return customer.Id;
    }

    public async Task<string> CreatePaymentAsync(PaymentModel model)
    {

        StripeConfiguration.ApiKey = _appOptions.StripeApi;


        var options = new PaymentIntentCreateOptions
        {
            Amount = (long) model.Invoice.TotalAmount.amount,
            Currency = "gbp",
            PaymentMethod = "pm_card_visa",
        };

        var service = new PaymentIntentService();

        PaymentIntent charge;

        try
        {
            charge = await service.CreateAsync(options);
        }
        catch (StripeException e)
        {
            throw new DomainValidationException(e.Message);
        }


        return charge.Id;
    }

    public Task<bool> DeleteCustomerAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<(string, string)> GetCurrentUserStripeId()
    {
        var user = _authService.GetCurrentUser();

        if (string.IsNullOrWhiteSpace(user.StripeId))
        {
            var stripeId = await CreateCustomerAsync(user.Email);
            var userModifiedEvent = new UserModifiedEvent(user.UserId, stripeId);

            await _mediator.Publish(userModifiedEvent);
            user.StripeId = stripeId;
        }

        return (user.StripeId, user.UserId);
    }
}