namespace Isu.Exceptions;

public class InvalidEducationException : IsuException
{
    public InvalidEducationException(int educationValue)
        : base($"{educationValue} - is incorrect education value") { }
}