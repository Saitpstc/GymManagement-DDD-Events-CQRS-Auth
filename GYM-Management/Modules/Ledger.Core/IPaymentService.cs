namespace Ledger.Core;

using Test;

public interface IPaymentService
{
    PaymentResult PayTheInvoice(PaymentModel model);
}