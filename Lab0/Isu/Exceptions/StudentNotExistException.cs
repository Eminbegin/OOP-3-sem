using Isu.Entities;

namespace Isu.Exceptions;

public class StudentNotExistException : Exception
{
    public StudentNotExistException(Student student)
        : base($"There is not student with name {student.Name}") { }
}