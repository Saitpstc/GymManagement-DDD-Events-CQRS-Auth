namespace Ledger.Infrastructure;

using Auth.Entry;
using Core;
using IntegrationEvents.LedgerModule;
using MediatR;
using Shared.Core;
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



        var options =new PaymentIntentCreateOptions
        {
            Amount = 543,
            Currency = "gbp",
            PaymentMethod = "pm_card_visa",
        }; /*new ChargeCreateOptions
        {
            Amount = (long) (model.Invoice.TotalAmount.amount * 100), // Amount in cents
            Currency = "usd",
            Description = model.Invoice.Description.Value,
            Source = new AnyOf<string, CardCreateNestedOptions>(new CardCreateNestedOptions()
            {
                Number = model.CreditCardInformation.CardNumber,
                Cvc = model.CreditCardInformation.Cvc,
                ExpMonth = model.CreditCardInformation.ExpMonth,
                ExpYear = model.CreditCardInformation.ExpYear,
                Name = model.CreditCardInformation.CardFullName,
                
            })
        };*/

        var service = new PaymentIntentService();
        var charge = await service.CreateAsync(options);

        return charge.Id;
    }

    public Task<bool> DeleteCustomerAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetCurrentUserStripeId()
    {
        var user = _authService.GetCurrentUser();

        if (string.IsNullOrWhiteSpace(user.StripeId))
        {
            var stripeId = await CreateCustomerAsync(user.Email);
            var userModifiedEvent = new UserModifiedEvent(user.UserId, stripeId);

            await _mediator.Publish(userModifiedEvent);
            user.StripeId = stripeId;
        }

        return user.StripeId;
    }
}