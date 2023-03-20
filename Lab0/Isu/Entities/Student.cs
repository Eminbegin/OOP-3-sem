using Isu.Exceptions;

namespace Isu.Entities;

public class Student : IEquatable<Student>
{
    public Student(string name, Group group, int isuId)
    {
        Name = name;
        Group = group;
        Group.AddStudent(this);
        IsuId = isuId;
    }

    public int IsuId { get; }
    public string Name { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        if (Group.Equals(newGroup))
        {
            throw new StudentExistException(this, newGroup.GroupName);
        }

        newGroup.AddStudent(this);
        Group.RemoveStudent(this);
        Group = newGroup;
    }

    public bool Equals(Student? other)
    {
        return other is not null && IsuId.Equals(other.IsuId);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Student);
    }

    public override int GetHashCode()
    {
        return IsuId.GetHashCode();
    }
}