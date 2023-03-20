namespace Isu.Exceptions;

public class InvalidCourseValueException : IsuException
{
    public InvalidCourseValueException(int courseValue)
        : base($"Wrong name: {courseValue} - is incorrect number of course") { }
}