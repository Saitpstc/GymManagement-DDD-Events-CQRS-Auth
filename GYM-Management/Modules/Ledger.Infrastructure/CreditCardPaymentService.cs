namespace Ledger.Infrastructure;

using Auth.Entry;
using Core;
using Shared.Core;
using Test;

public class CreditCardPaymentService:IPaymentService
{
    private readonly IStripeService _stripeService;

    public CreditCardPaymentService(IStripeService stripeService)
    {
        _stripeService = stripeService;


    }

    public PaymentResult PayTheInvoice(PaymentModel model)
    {

        //var stripeUserId = _stripeService.GetCurrentUserStripeId();

        _stripeService.CreatePaymentAsync(model);
        model.Invoice.PayTheInvoice(model);


        return new PaymentResult()
        {
            IsSuccess = true
        };
    }
}