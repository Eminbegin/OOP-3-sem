namespace Isu.Exceptions;

public class InvalidGroupNameLenghtException : IsuException
{
    public InvalidGroupNameLenghtException()
        : base("Incorrect name of GroupName") { }
}