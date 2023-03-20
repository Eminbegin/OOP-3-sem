using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Transactions;

public class TransferTransaction : ITransaction
{
    private readonly IAccount _accountFrom;
    private readonly IAccount _accountTo;

    public TransferTransaction(decimal money, DateTime transactionTime, int id, IAccount accountFrom, IAccount accountTo)
    {
        Id = id;
        Money = money;
        TransactionTime = transactionTime;
        IsCancelled = false;
        IsPerformed = false;
        _accountFrom = accountFrom;
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
            if (!_accountFrom.IsDeductPossible(Money))
            {
                throw ActionExceptions.ImpossibleWithdraw(_accountFrom.Id);
            }

            if (!_accountTo.IsDepositPossible(Money))
            {
                throw ActionExceptions.ImpossibleDeposit(_accountTo.Id);
            }

            _accountFrom.DeductFromAccount(Money);
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
            _accountFrom.DepositIntoAccount(Money);
            IsCancelled = true;
        }
        else
        {
            throw ActionExceptions.ImpossibleCancel(Id);
        }
    }
}