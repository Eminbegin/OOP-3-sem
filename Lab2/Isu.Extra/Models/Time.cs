using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public record Time
{
    private const int MinClassNumber = 1;
    private const int MaxClassNumber = 8;
    private int _classNumber;

    public Time(DayOfWeek weekDay, int classNumber)
    {
        WeekDay = weekDay;
        ClassNumber = classNumber;
    }

    public DayOfWeek WeekDay { get; }

    public int ClassNumber
    {
        get => _classNumber;
        set
        {
            ValidateClassNumber(value);
            _classNumber = value;
        }
    }

    private void ValidateClassNumber(int value)
    {
        if (value is not(>= MinClassNumber and <= MaxClassNumber))
        {
            throw InvalidScheduleException.InvalidClassNumber();
        }
    }
}