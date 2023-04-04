namespace Ledger.Core;

using Shared.Core.Exceptions;
using Test;

public class Invoice
{
    public TotalAmount TotalAmount { get; }
    public DateTime DueDate { get; private set; }
    public Description? Description { get; private set; }
    public InvoiceStatus Status { get; private set; }
    public Guid? PayerUserId { get; private set; }

    public Invoice(TotalAmount totalAmount, Description? description = null)
    {
        DueDate = DateTime.Now.AddDays(7);
        Description = description;
        TotalAmount = totalAmount;
        Status = InvoiceStatus.Waiting;
    }

    public void SetDescription(Description description)
    {
        Description = description;
    }

    public void ChangeStatus(InvoiceStatus newStatus)
    {
        if (Status == InvoiceStatus.Paid && newStatus == InvoiceStatus.Canceled)
        {
            throw new DomainValidationException("Invoice already paid ");
        }

        if (newStatus == InvoiceStatus.Paid && PayerUserId is null)
        {
            throw new DomainValidationException("Please use pay method to change status for paid statuses");
        }
        Status = newStatus;
    }

    public void PostponeDueDate()
    {
        DueDate = DueDate.AddDays(7);
        Status = InvoiceStatus.Postponed;
    }

    public void PayTheInvoice(PaymentModel paymentModel)
    {
        PayerUserId = paymentModel.PayerUserId;
        Status = InvoiceStatus.Paid;
    }
}