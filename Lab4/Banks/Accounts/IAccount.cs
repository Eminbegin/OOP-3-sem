using Banks.Observers.ChangingData;
using Banks.Observers.SkipDays;

namespace Banks.Accounts;

public interface IAccount : IChangingDataObserver, ISkippingTimeObserver
{
    public int Id { get; }
    public decimal Balance { get; }
    public decimal LimitForDoubtful { get; }
    bool IsDepositPossible(decimal money);
    bool IsDeductPossible(decimal money);
    void DepositIntoAccount(decimal value);
    void DeductFromAccount(decimal value);
}