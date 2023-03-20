using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions;

public class DepositTransaction : ITransaction
{
    private readonly IAccount _accountTo;
    public DepositTransaction(decimal money, DateTime transactionTime, int id, IAccount accountTo)
    {
        Id = id;
        Money = money;
        TransactionTime = transactionTime;
        IsCancelled = false;
        IsPerformed = false;
        _accountTo = accountTo;
    }

    public int Id { get; }
    public decimal Money { get; }
    public DateTime TransactionTime { get; }
    public bool IsCancelled { get; private set; }
    public bool IsPerformed { get; private set; }

    public void Perform()
    {
        if (!IsPerformed)
        {
            if (!_accountTo.IsDepositPossible(Money))
            {
                throw ActionExceptions.ImpossibleWithdraw(_accountTo.Id);
            }

            _accountTo.DepositIntoAccount(Money);
            IsPerformed = true;
        }
        else
        {
            throw ActionExceptions.ImpossiblePerform(Id);
        }
    }

    public void Cancel()
    {
        if (!IsCancelled && IsPerformed)
        {
            _accountTo.DeductFromAccount(Money);
            IsCancelled = true;
        }
        else
        {
            throw ActionExceptions.ImpossibleCancel(Id);
        }
    }
}