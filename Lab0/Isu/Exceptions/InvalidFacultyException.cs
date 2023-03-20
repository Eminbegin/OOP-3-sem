namespace Isu.Exceptions;

public class InvalidFacultyException : IsuException
{
    public InvalidFacultyException(char facultyValue)
        : base($"{facultyValue} - is incorrect faculty value") { }
}