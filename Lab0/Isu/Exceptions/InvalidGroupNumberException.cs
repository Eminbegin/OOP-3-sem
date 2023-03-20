namespace Isu.Exceptions;

public class InvalidGroupNumberException : IsuException
{
    public InvalidGroupNumberException(int educationValue)
        : base($"{educationValue} - is incorrect education value") { }
}