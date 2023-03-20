using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class StudentExistException : IsuException
{
    public StudentExistException(Student student, GroupName groupName)
        : base($"Student {student.Name} already exist in Group {groupName.Name}") { }
}