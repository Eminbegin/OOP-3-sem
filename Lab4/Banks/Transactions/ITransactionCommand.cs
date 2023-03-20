namespace Banks.Transactions;

public interface ITransactionCommand
{
    void Perform();
    void Cancel();
}