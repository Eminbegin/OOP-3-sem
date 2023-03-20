namespace Isu.Exceptions;

public class WrongStudentIdException : IsuException
{
    public WrongStudentIdException(int id)
        : base($"There is not student with id {id}") { }
}