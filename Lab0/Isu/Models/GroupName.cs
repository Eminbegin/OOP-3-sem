using Isu.Exceptions;

namespace Isu.Models;

public record GroupName
{
    private const int MinNameLength = 5;
    private const int MaxNameLength = 7;

    public GroupName(string name)
    {
        Name = name;
        FacultyNumber = new FacultyNumber(name);
        CourseNumber = new CourseNumber(name);
        EducationNumber = new EducationNumber(name);
        GroupNumber = new GroupNumber(name);
        if (name.Length is not(>= MinNameLength and <= MaxNameLength))
        {
            throw new InvalidGroupNameLenghtException();
        }
    }

    public string Name { get; }
    public FacultyNumber FacultyNumber { get; }
    public EducationNumber EducationNumber { get; }
    public CourseNumber CourseNumber { get; }
    public GroupNumber GroupNumber { get; }
}