using Banks.BanksSystem.BankConfigurations;

namespace Banks.Accounts;

public class DepositAccount : IAccount
{
    private readonly DepositPercentages _depositPercentages;
    private readonly int _depositDays;
    private IVerificationStrategy _verification;
    private int _pastDays = 0;
    private decimal _amount = 0;
    public DepositAccount(int id, decimal balance, decimal limitForDoubtful, DepositPercentages depositPercentages, int depositDays, IVerificationStrategy verification)
    {
        Id = id;
        Balance = balance;
        LimitForDoubtful = limitForDoubtful;
        _depositPercentages = depositPercentages;
        _depositDays = depositDays;
        _verification = verification;
    }

    public int Id { get; }
    public decimal Balance { get; set; }
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
        if (_pastDays < _depositDays || !_verification.IsDeductPossible(LimitForDoubtful, money) || Balance - money < decimal.Zero)
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
        if (_pastDays >= _depositDays) return;
        _amount += Balance * _depositPercentages.GetPercentage(Balance);
        _pastDays += 1;
    }

    public void UpdateBalance()
    {
        Balance += _amount;
    }
}