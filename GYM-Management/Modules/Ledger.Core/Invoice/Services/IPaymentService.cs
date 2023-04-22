namespace Ledger.Core;

using Test;

public interface IPaymentService
{
    Task<PaymentResult> PayTheInvoice(PaymentModel model);
}