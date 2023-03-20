using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public interface IIsuServiceExtra
{
    ExtraGroup AddGroup(string name);
    ExtraStudent AddStudent(ExtraGroup extraGroup, string name);
    Teacher AddTeacher(string name);
    ExtraStudent? FindExtraStudent(int id);
    ExtraStudent GetExtraStudent(int id);
    Teacher? FindTeacher(int id);
    Teacher GetTeacher(int id);
    ExtraGroup? FindExtraGroup(GroupName groupName);
    ExtraGroup GetExtraGroup(GroupName groupName);
    void ChangeScheduleToGroup(ExtraGroup extraGroup, Schedule schedule);
    void SubscribeStudentToExtraStudy(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision);
    void ChangeStudentGroup(ExtraStudent extraStudent, ExtraGroup extraGroup);
    ExtraStudy AddExtraStudy(string name, string megaFacultyName);
    ExtraStudyDivision AddExtraStudyDivision(string name, ExtraStudy extraStudy, Lesson lesson);
    void SubscribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision);
    void DescribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision);
    IReadOnlyCollection<ExtraStudyDivision> GetExtraStudyDivisions(string name);
    IReadOnlyCollection<ExtraStudent> GetStudentsFromDivision(string name);
    IReadOnlyCollection<ExtraStudent> GetStudentsWithOutExtraStudy();
}