namespace Isu.Extra.Exceptions;

public class InvalidScheduleException : Exception
{
    private InvalidScheduleException(string message)
        : base(message)
    {
    }

    public static InvalidScheduleException LessonsIntercet()
        => new InvalidScheduleException("There is intersecion of lessons");

    public static InvalidScheduleException InvalidClassNumber()
        => new InvalidScheduleException("Number of class can be between 1 and 8");
}