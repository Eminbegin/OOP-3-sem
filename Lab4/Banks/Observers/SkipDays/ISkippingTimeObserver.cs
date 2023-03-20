namespace Banks.Observers.SkipDays;

public interface ISkippingTimeObserver
{
    void UpdateAmount();
    void UpdateBalance();
}