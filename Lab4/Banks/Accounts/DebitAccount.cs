namespace Banks.Accounts;

public class DebitAccount : IAccount
{
    private readonly decimal _debitPercentage;
    private decimal _amount;
    private IVerificationStrategy _verification;

    public DebitAccount(int id, decimal balance, decimal debitPercentage, decimal limitForDoubtful, IVerificationStrategy verification)
    {
        Id = id;
        Balance = balance;
        _debitPercentage = debitPercentage / 100;
        LimitForDoubtful = limitForDoubtful;
        _amount = 0;
        _verification = verification;
    }

    public int Id { get; }

    public decimal Balance { get; private set; }

    public decimal LimitForDoubtful { get; }

    public bool IsDepositPossible(decimal money)
    {
        if (money < decimal.Zero)
        {
            return false;
        }

        return true;
    }

    public bool IsDeductPossible(decimal money)
    {
        if (!_verification.IsDeductPossible(LimitForDoubtful, money) || Balance - money < decimal.Zero || money < decimal.Zero)
        {
            return false;
        }

        return true;
    }

    public void DepositIntoAccount(decimal value)
    {
        Balance += value;
    }

    public void DeductFromAccount(decimal value)
    {
        Balance -= value;
    }

    public void UpdateVerification(IVerificationStrategy verification)
    {
        _verification = verification;
    }

    public void UpdateAmount()
    {
        _amount += Balance * _debitPercentage;
    }

    public void UpdateBalance()
    {
        Balance += _amount;
    }
}