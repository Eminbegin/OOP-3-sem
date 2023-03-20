using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group : IEquatable<Group>
{
    private const int MaxStudents = 3;

    private readonly List<Student> _students = new ();

    public Group(GroupName groupName)
    {
        GroupName = groupName;
    }

    public GroupName GroupName { get; }

    public IReadOnlyCollection<Student> Students => _students;
    public int StudentLimit => MaxStudents;

    public void AddStudent(Student student)
    {
        if (IsFull())
        {
            throw new ExceededLimitStudentsException(GroupName);
        }

        if (Students.Any(s => s.Equals(student)))
        {
            throw new StudentExistException(student, GroupName);
        }

        _students.Add(student);
    }

    public bool IsFull()
    {
        return _students.Count >= MaxStudents;
    }

    public void RemoveStudent(Student student)
    {
        if (!Students.Any(s => s.Equals(student)))
        {
            throw new StudentIsNotInGroupException(student, GroupName);
        }

        _students.Remove(student);
    }

    public bool Equals(Group? other)
    {
        return other is not null &&
               GroupName.Equals(other.GroupName);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Group);
    }

    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }
}