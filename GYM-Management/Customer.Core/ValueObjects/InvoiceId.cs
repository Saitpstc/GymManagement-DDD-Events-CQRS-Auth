namespace Customer.Core.ValueObjects;

using Shared.Core.Domain;

internal record InvoiceId:ValueObject
{
    public Guid Value { get; private set; }

    private InvoiceId() {}
}