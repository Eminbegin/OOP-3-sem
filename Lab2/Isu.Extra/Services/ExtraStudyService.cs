using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public class ExtraStudyService : IExtraStudyService
{
    private readonly List<ExtraStudy> _extraStudies;
    private List<MegaFaculty> _megaFaculties;

    public ExtraStudyService(List<MegaFaculty> megaFaculties)
    {
        _extraStudies = new List<ExtraStudy>();
        _megaFaculties = megaFaculties;
    }

    public ExtraStudy AddExtraStudy(string name, MegaFaculty megaFaculty)
    {
        if (IsExtraStudyExist(name))
        {
            throw ExtraStudyServiceException.ExtraStudyAlreadyExist(name);
        }

        var extraStudy = new ExtraStudy(name, megaFaculty);
        _extraStudies.Add(extraStudy);
        return extraStudy;
    }

    public ExtraStudyDivision AddExtraStudyDivision(string name, ExtraStudy extraStudy, Lesson lesson)
    {
        if (!IsExtraStudyExist(extraStudy))
        {
            throw ExtraStudyServiceException.ExtraStudyNotExist(extraStudy);
        }

        var division = new ExtraStudyDivision(name, extraStudy, lesson);
        return division;
    }

    public void SubscribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision, Schedule schedule)
    {
        if (IsIntersect(extraStudyDivision.Lesson, schedule))
        {
            throw ExtraStudyServiceException.ExtraStudyHasIntersection(extraStudyDivision, extraStudent);
        }

        extraStudyDivision.AddStudent(extraStudent);
    }

    public void DescribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision)
    {
        extraStudyDivision.RemoveStudent(extraStudent);
    }

    public bool IsExtraStudyNotIntersect(ExtraStudent extraStudent, Schedule schedule, ExtraStudyDivision extraStudyDivision)
    {
        if (extraStudent.GetDivisionsCount() == 0 || IsEmpty(schedule))
        {
            return true;
        }

        if (IsIntersect(extraStudyDivision.Lesson, schedule))
        {
            return false;
        }

        return true;
    }

    public void ChangeIntersectionExtraStudy(ExtraStudent extraStudent, Schedule schedule, ExtraStudyDivision extraStudyDivision)
    {
        ArgumentNullException.ThrowIfNull(extraStudyDivision, "Impossible change null extra study");
        ExtraStudy extraStudy = _extraStudies.First(es => es.Divisions.Contains(extraStudyDivision));
        ExtraStudyDivision? newDivision = extraStudy.Divisions.FirstOrDefault(d => !IsIntersect(d.Lesson, schedule));
        extraStudyDivision.RemoveStudent(extraStudent);
        if (newDivision is not null)
        {
            extraStudyDivision.AddStudent(extraStudent);
        }
    }

    public IReadOnlyCollection<ExtraStudyDivision> GetExtraStudyDivisions(string name)
    {
        ExtraStudy? extraStudy = _extraStudies.FirstOrDefault(es => es.Name.Equals(name));
        if (extraStudy is null)
        {
            throw ExtraStudyServiceException.ExtraStudyNotExist(name);
        }

        return extraStudy.Divisions;
    }

    public IReadOnlyCollection<ExtraStudent> GetStudentsFromDivision(string name)
    {
        ExtraStudy? extraStudy = _extraStudies
            .FirstOrDefault(es => es
                .Divisions.Any(d => d.Name.Equals(name)));
        if (extraStudy is null)
        {
            throw ExtraStudyServiceException.ExtraStudyNotExist(name);
        }

        return extraStudy.Divisions.First(d => d.Name.Equals(name)).Students;
    }

    private bool IsExtraStudyExist(string name)
        => _extraStudies.Any(j => j.Name.Equals(name));

    private bool IsEmpty(Schedule schedule)
    {
        if (!schedule.Lessons.Any())
        {
            return true;
        }

        return false;
    }

    private bool IsIntersect(Lesson lesson, Schedule schedule)
    {
        IReadOnlyCollection<Lesson> lessons = schedule.Lessons;
        if (lessons.Any(l => l.Time.Equals(lesson.Time)))
        {
            return true;
        }

        return false;
    }

    private bool IsExtraStudyExist(ExtraStudy extraStudy)
    {
        if (_extraStudies.Contains(extraStudy))
        {
            return true;
        }

        return false;
    }
}