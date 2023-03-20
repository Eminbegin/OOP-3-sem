using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Utility;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private readonly List<Group> _groups;
    private readonly List<Student> _students;
    private readonly IdGenerator _idGenerator;

    public IsuService(IdGenerator idGenerator)
    {
        _groups = new List<Group>();
        _students = new List<Student>();
        _idGenerator = idGenerator;
    }

    public Group AddGroup(GroupName name)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible add group with null GroupName");
        if (_groups.Any(g => g.GroupName.Equals(name)))
        {
            throw new SameGroupNameException(name);
        }

        var group = new Group(name);
        _groups.Add(group);
        return group;
    }

    public Student AddStudent(Group group, string name)
    {
        ArgumentNullException.ThrowIfNull(group, "Impossible add student with null Group");
        ArgumentNullException.ThrowIfNull(name, "Impossible add student with null Name");
        if (!_groups.Any(g => g.Equals(group)))
        {
            throw new GroupNotExistException(group.GroupName);
        }

        var student = new Student(name, group, _idGenerator.Next());
        _students.Add(student);
        return student;
    }

    public Student GetStudent(int id)
    {
        Student? student = FindStudent(id);
        if (student is null)
        {
            throw new WrongStudentIdException(id);
        }

        return student;
    }

    public Student? FindStudent(int id)
        => _students.FirstOrDefault(s => s.IsuId == id);

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        CheckGroupExist(groupName);
        return _groups.First(g => g.GroupName.Equals(groupName)).Students;
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber) =>
        _students.Where(s => s.Group.GroupName.CourseNumber.Equals(courseNumber)).ToList();

    public Group? FindGroup(GroupName groupName) =>
        _groups.FirstOrDefault(g => g.GroupName.Equals(groupName));

    public Group GetGroup(GroupName groupName) =>
        FindGroup(groupName) ??
        throw new GroupNotGetException(groupName);

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber) =>
        _groups.Where(g => g.GroupName.CourseNumber.Equals(courseNumber)).ToList();

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        ArgumentNullException.ThrowIfNull(newGroup, "Impossible add student with null Group");
        ArgumentNullException.ThrowIfNull(student, "Impossible change student's group for null Student");
        CheckStudentExist(student);
        CheckGroupExist(newGroup);
        student.ChangeGroup(newGroup);
    }

    private void CheckStudentExist(Student student)
    {
        if (!_students.Any(s => s.Equals(student)))
        {
            throw new StudentNotExistException(student);
        }
    }

    private void CheckGroupExist(Group group)
    {
        if (!_groups.Any(g => g.Equals(group)))
        {
            throw new GroupNotExistException(group.GroupName);
        }
    }

    private void CheckGroupExist(GroupName groupName)
    {
        if (!_groups.Any(g => g.GroupName.Equals(groupName)))
        {
            throw new GroupNotExistException(groupName);
        }
    }
}