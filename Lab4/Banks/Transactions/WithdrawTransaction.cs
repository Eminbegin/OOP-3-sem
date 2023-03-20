using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions;

public class WithdrawTransaction : ITransaction
{
    private readonly IAccount _accountFrom;
    public WithdrawTransaction(decimal money, DateTime transactionTime, int id, IAccount accountFrom)
    {
        Id = id;
        Money = money;
        TransactionTime = transactionTime;
        IsCancelled = false;
        IsPerformed = false;
        _accountFrom = accountFrom;
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
            if (!_accountFrom.IsDeductPossible(Money))
            {
                throw ActionExceptions.ImpossibleWithdraw(_accountFrom.Id);
            }

            _accountFrom.DeductFromAccount(Money);
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
            _accountFrom.DepositIntoAccount(Money);
            IsCancelled = true;
        }
        else
        {
            throw ActionExceptions.ImpossibleCancel(Id);
        }
    }
}
