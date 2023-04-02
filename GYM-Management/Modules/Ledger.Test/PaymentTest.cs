namespace Ledger.Test;

using Core;
using FluentAssertions;

public class PaymentTest
{

    [Fact]
    public void Credit_Card_Payment_Test()
    {
        IPaymentService paymentService = new CreditCardPaymentService();

        var model = new PaymentModel();

        model.Invoice = new Invoice(new TotalAmount(100));
        model.PayerUserId = Guid.NewGuid();
        model.CreditCardInformation = new CreditCardInformation();

        PaymentResult result = paymentService.PayTheInvoice(model);

        model.Invoice.Status.Should().Be(InvoiceStatus.Paid);
        result.IsSuccess.Should().Be(true);

    }
}

public class CreditCardPaymentService:IPaymentService
{
    public PaymentResult PayTheInvoice(PaymentModel model)
    {
        model.Invoice.PayTheInvoice(model);

        return new PaymentResult()
        {
            IsSuccess = true
        };
    }
}

public class PaymentResult
{
    public object IsSuccess { get; set; }
}


public interface IPaymentService
{
    PaymentResult PayTheInvoice(PaymentModel model);
}