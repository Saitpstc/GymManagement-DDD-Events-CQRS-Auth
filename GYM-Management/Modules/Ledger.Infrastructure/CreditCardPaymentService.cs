namespace Ledger.Infrastructure;

using Core;
using Shared.Core;
using Test;

public class CreditCardPaymentService:IPaymentService
{
    private readonly AppOptions _appOptions;

    public CreditCardPaymentService(AppOptions appOptions)
    {
        _appOptions = appOptions;

    }
    public PaymentResult PayTheInvoice(PaymentModel model)
    {
        
        
        model.Invoice.PayTheInvoice(model);

        return new PaymentResult()
        {
            IsSuccess = true
        };
    }
}