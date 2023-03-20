using Isu.Entities;
using Isu.Extra.Models;
namespace Isu.Extra.Entities;

public class ExtraStudent : IEquatable<ExtraStudent>
{
    private const int MaxExtraStudyDivisionCount = 2;
    private const int MinExtraStudyDivisionCount = 0;
    private List<ExtraStudyDivision> _extraStudyDivisions;

    public ExtraStudent(Student student, MegaFaculty megaFaculty)
    {
        Student = student;
        MegaFaculty = megaFaculty;
        _extraStudyDivisions = new List<ExtraStudyDivision>();
    }

    public Student Student { get; }
    public MegaFaculty MegaFaculty { get; }
    public IReadOnlyCollection<ExtraStudyDivision> Divisions => _extraStudyDivisions;

    public void AddDivision(ExtraStudyDivision extraStudyDivision)
    {
        if (_extraStudyDivisions.Count > MaxExtraStudyDivisionCount)
        {
            throw new Exception();
        }

        _extraStudyDivisions.Add(extraStudyDivision);
    }

    public void RemoveDivision(ExtraStudyDivision extraStudyDivision)
    {
        if (_extraStudyDivisions.Count == MinExtraStudyDivisionCount)
        {
            throw new Exception();
        }

        if (!_extraStudyDivisions.Remove(extraStudyDivision))
        {
            throw new Exception();
        }
    }

    public int GetDivisionsCount()
    {
        return _extraStudyDivisions.Count;
    }

    public bool Equals(ExtraStudent? other)
    {
        return other is not null && Student.Equals(other.Student);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ExtraStudent);
    }

    public override int GetHashCode()
    {
        return Student.GetHashCode();
    }
}