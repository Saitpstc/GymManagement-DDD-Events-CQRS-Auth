namespace Customer.Core.ValueObjects;

using Shared.Core.Domain;

record InvoiceId:ValueObject
{

    private InvoiceId() {}

    public Guid Value { get; private set; }
}