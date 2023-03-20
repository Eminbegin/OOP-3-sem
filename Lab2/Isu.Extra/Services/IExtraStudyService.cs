using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IExtraStudyService
{
    ExtraStudy AddExtraStudy(string name, MegaFaculty megaFaculty);
    ExtraStudyDivision AddExtraStudyDivision(string name, ExtraStudy extraStudy, Lesson lesson);
    void SubscribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision, Schedule schedule);
    void DescribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision);
    bool IsExtraStudyNotIntersect(ExtraStudent extraStudent, Schedule schedule, ExtraStudyDivision extraStudyDivision);
    void ChangeIntersectionExtraStudy(ExtraStudent extraStudent, Schedule schedule, ExtraStudyDivision extraStudyDivision);
    IReadOnlyCollection<ExtraStudyDivision> GetExtraStudyDivisions(string name);
    IReadOnlyCollection<ExtraStudent> GetStudentsFromDivision(string name);
}