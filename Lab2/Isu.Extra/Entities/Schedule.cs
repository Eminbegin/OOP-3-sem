using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Schedule
{
    private IReadOnlyCollection<Lesson> _lessons;

    public Schedule(IReadOnlyCollection<Lesson> lessons)
    {
        _lessons = lessons;
    }

    public static Schedule Empty => new Schedule(Array.Empty<Lesson>());
    public static ScheduleBuilder Builder => new ScheduleBuilder();

    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public class ScheduleBuilder
    {
        private readonly List<Lesson> _lessons = new List<Lesson>();

        public ScheduleBuilder AddLesson(Lesson lesson)
        {
            CheckIntersection(lesson, _lessons);
            _lessons.Add(lesson);
            return this;
        }

        public Schedule Build()
        {
            return new Schedule(_lessons);
        }

        private void CheckIntersection(Lesson lesson, List<Lesson> lessons)
        {
            if (lessons.Any(l => l.Equals(lesson)))
            {
                throw InvalidScheduleException.LessonsIntercet();
            }
        }
    }
}