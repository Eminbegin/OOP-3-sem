using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public record Lesson
{
    public Lesson(Teacher teacher, int auditorium, DayOfWeek dayOfWeek, int classNumber)
    {
        Auditorium = auditorium;
        Teacher = teacher;
        Time = new Time(dayOfWeek, classNumber);
    }

    public int Auditorium { get; }
    public Time Time { get; }
    public Teacher Teacher { get; }
}