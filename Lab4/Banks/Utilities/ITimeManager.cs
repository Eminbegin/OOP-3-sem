namespace Banks.Utilities;

public interface ITimeManager
{
    DateTime Time { get; }
    void NextDay();
    bool IsTheFirstDayOfMonth();
}