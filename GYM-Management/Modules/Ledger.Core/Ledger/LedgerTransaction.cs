namespace Ledger.Core;

using Test;

public class LedgerTransaction
{
    public LedgerTransaction(Operation operation, Amount amount,Description? description=null )
    {
        Description = description;
        Operation = operation;
        Amount = amount;
        TransactionDate=DateTime.Now;
    }

    public Description? Description { get;  }
    public Operation Operation { get; }
    public Amount Amount { get; }
    public DateTime TransactionDate { get; }
}