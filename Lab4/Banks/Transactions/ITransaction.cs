namespace Banks.Transactions;

public interface ITransaction : ITransactionCommand
{
    public int Id { get; }
    public decimal Money { get; }
    public DateTime TransactionTime { get; }
    bool IsCancelled { get; }
    bool IsPerformed { get; }
}