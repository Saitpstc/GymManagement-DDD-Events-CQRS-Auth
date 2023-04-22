namespace Ledger.Core;

public interface IStripeService
{
    Task<string> CreateCustomerAsync(string userEmail);

    Task<string> CreatePaymentAsync(PaymentModel model);

    Task<bool> DeleteCustomerAsync(string customerId);

    Task<(string, string)> GetCurrentUserStripeId();
}