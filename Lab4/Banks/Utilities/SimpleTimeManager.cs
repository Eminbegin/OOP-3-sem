namespace Banks.Utilities;

public class SimpleTimeManager : ITimeManager
{
    public SimpleTimeManager(DateTime dateTime)
    {
        Time = dateTime;
    }

    public DateTime Time { get; private set; }

    public void NextDay()
    {
        Time = Time.AddDays(1);
    }

    public bool IsTheFirstDayOfMonth()
    {
        return Time.Day == 1;
    }
}