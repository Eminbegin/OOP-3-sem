namespace Banks.Observers.SkipDays;

public interface ISkippingTimeObservable
{
    void AddSkippingDayObserver(ISkippingTimeObserver observer);
    void RemoveSkippingDayObserver(ISkippingTimeObserver observer);
    void SkippingDay();
    void MonthPassed();
}