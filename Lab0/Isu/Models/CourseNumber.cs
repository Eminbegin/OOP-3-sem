using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber : IEquatable<CourseNumber>
{
    private const int CourseNumberPosition = 2;
    private const int MinCourse = 1;
    private const int MaxCourse = 4;
    public CourseNumber(string name)
    {
        Course = CourseNumberChecker(name);
    }

    public int Course { get; }

    public bool Equals(CourseNumber? other)
    {
        return other is not null && Course == other.Course;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as CourseNumber);
    }

    public override int GetHashCode()
    {
        return Course;
    }

    private int CourseNumberChecker(string name)
    {
        if (!int.TryParse(name[CourseNumberPosition].ToString(), out int courseValue))
        {
            throw new InvalidCourseValueException(courseValue);
        }

        if (courseValue > MaxCourse || courseValue < MinCourse)
        {
            throw new InvalidCourseValueException(courseValue);
        }

        return courseValue;
    }
}