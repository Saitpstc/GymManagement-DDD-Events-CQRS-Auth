namespace Ledger.Test;

using Core;
using FluentAssertions;
using Infrastructure;

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