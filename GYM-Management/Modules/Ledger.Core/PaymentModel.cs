namespace Ledger.Core;

using Test;

public class PaymentModel
{
    public Invoice Invoice { get; set; }
    
    public Guid PayerUserId { get; set; }
    public string StripeId { get; set; }

}