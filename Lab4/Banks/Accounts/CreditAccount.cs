namespace Banks.Accounts;

public class CreditAccount : IAccount
{
    private readonly decimal _creditPercentage;
    private readonly decimal _creditLimit;
    private IVerificationStrategy _verification;
    private decimal _amount = 0;
    public CreditAccount(int id, decimal balance, decimal creditPercentage, decimal limitForDoubtful, decimal creditLimit, IVerificationStrategy verification)
    {
        Id = id;
        Balance = balance;
        _creditPercentage = creditPercentage / 100;
        LimitForDoubtful = limitForDoubtful;
        _creditLimit = creditLimit;
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
        if (!_verification.IsDeductPossible(LimitForDoubtful, money) || money < decimal.Zero || Balance - money < _creditLimit)
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
        if (Balance < 0)
        {
            _amount += Balance * _creditPercentage;
        }
    }

    public void UpdateBalance()
    {
        Balance += _amount;
    }
}