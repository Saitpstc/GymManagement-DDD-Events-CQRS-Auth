namespace Ledger.Test;

using Core;

public class PaymentModel
{
    public Invoice Invoice { get; set; }
    public Guid PayerUserId { get; set; }
    public CreditCardInformation CreditCardInformation { get; set; }
}