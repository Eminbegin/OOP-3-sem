using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class ExtraStudyDivision : IEquatable<ExtraStudyDivision>
{
    private List<ExtraStudent> _students;

    public ExtraStudyDivision(string name, ExtraStudy extraStudy, Lesson lesson)
    {
        _students = new List<ExtraStudent>();
        Lesson = lesson;
        Name = name;
        ExtraStudy = extraStudy;
        extraStudy.AddDivision(this);
    }

    public ExtraStudy ExtraStudy { get; }
    public string Name { get; }
    public Lesson Lesson { get; }
    public IReadOnlyCollection<ExtraStudent> Students => _students;

    public void AddStudent(ExtraStudent extraStudent)
    {
        if (_students.Any(s => s.Equals(extraStudent)))
        {
            throw InvalidExtraStudyException.DivisionContainsStudent(this, extraStudent);
        }

        extraStudent.AddDivision(this);
        _students.Add(extraStudent);
    }

    public void RemoveStudent(ExtraStudent extraStudent)
    {
        if (!_students.Remove(extraStudent))
        {
            throw InvalidExtraStudyException.DivisionNotContainsStudent(this, extraStudent);
        }

        extraStudent.RemoveDivision(this);
    }

    public bool Equals(ExtraStudyDivision? other)
    {
        return other is not null && Name == other.Name && Lesson.Equals(other.Lesson);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ExtraStudyDivision);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Lesson);
    }
}