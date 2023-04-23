namespace Ledger.Infrastructure;

using Auth.Entry;
using Core;
using IntegrationEvents.LedgerModule;
using MediatR;
using Shared.Core;
using Shared.Core.Exceptions;
using Stripe;

internal class StripeService:IStripeService
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

        CustomerCreateOptions customerOptions = new CustomerCreateOptions
        {
            Email = userEmail
        };

        CustomerService customerService = new CustomerService();
        Customer? customer = await customerService.CreateAsync(customerOptions);

        return customer.Id;
    }

    public async Task<string> CreatePaymentAsync(PaymentModel model)
    {

        StripeConfiguration.ApiKey = _appOptions.StripeApi;


        PaymentIntentCreateOptions options = new PaymentIntentCreateOptions
        {
            Amount = (long) model.Invoice.Amount.amount,
            Currency = "gbp",
            PaymentMethod = "pm_card_visa"
        };

        PaymentIntentService service = new PaymentIntentService();

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
        UserModel user = _authService.GetCurrentUser();

        if (string.IsNullOrWhiteSpace(user.StripeId))
        {
            var stripeId = await CreateCustomerAsync(user.Email);
            UserModifiedEvent userModifiedEvent = new UserModifiedEvent(user.UserId, stripeId);

            await _mediator.Publish(userModifiedEvent);
            user.StripeId = stripeId;
        }

        return (user.StripeId, user.UserId);
    }
}