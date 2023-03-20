using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class StudentIsNotInGroupException : IsuException
{
    public StudentIsNotInGroupException(Student student, GroupName groupName)
        : base($"Student {student.Name} don't exist in Group {groupName.Name}") { }
}