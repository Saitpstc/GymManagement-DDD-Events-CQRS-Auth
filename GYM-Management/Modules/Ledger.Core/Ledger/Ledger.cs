namespace Ledger.Core;

using Shared.Core.Domain;

public class Ledger:AggregateRoot
{

    public Ledger()
    {
        Balance = 0;
        Transactions = new List<LedgerTransaction>();

    }

    public List<LedgerTransaction> Transactions { get; }
    public double Balance { get; private set; }

    public void AddTransactionRecord(LedgerTransaction transaction)
    {
        Transactions.Add(transaction);
        Balance += transaction.Amount.amount;
    }
}