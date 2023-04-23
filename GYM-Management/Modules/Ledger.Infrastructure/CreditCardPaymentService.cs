namespace Ledger.Infrastructure;

using Core;
using Test;

internal class CreditCardPaymentService:IPaymentService
{
    private readonly IStripeService _stripeService;

    public CreditCardPaymentService(IStripeService stripeService)
    {
        _stripeService = stripeService;


    }

    public async Task<PaymentResult> PayTheInvoice(PaymentModel model)
    {

        (string, string) stripeUserId = await _stripeService.GetCurrentUserStripeId();

        model.StripeId = stripeUserId.Item1;
        model.PayerUserId = new Guid(stripeUserId.Item2);

        await _stripeService.CreatePaymentAsync(model);

        model.Invoice.PayTheInvoice(model);


        return new PaymentResult
        {
            IsSuccess = true
        };
    }
}