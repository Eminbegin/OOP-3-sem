using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTests
{
    private readonly IsuServiceExtra _isuServiceExtra = new IsuServiceExtra();

    [Theory]
    [InlineData("M3100", "Aboba Abobavich")]
    [InlineData("M32340", "Mem Memov")]
    [InlineData("P4222", "Dora")]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string groupName, string studentName)
    {
        ExtraGroup extraGroup = _isuServiceExtra.AddGroup(groupName);
        ExtraStudent extraStudent = _isuServiceExtra.AddStudent(extraGroup, studentName);
        Assert.Equal(extraGroup.Group, extraStudent.Student.Group);
        Assert.Contains(extraStudent.Student, extraGroup.Group.Students);
    }

    [Fact]
    public void AddScheduleToGroup_GroupHasSchedule()
    {
        ExtraGroup extraGroup = _isuServiceExtra.AddGroup("M32001");
        Teacher teacher = _isuServiceExtra.AddTeacher("Shrek");
        Schedule schedule = CreateSchedule1();
        ExtraGroup extraGroup1 = _isuServiceExtra.AddGroup("M32011");
        _isuServiceExtra.ChangeScheduleToGroup(extraGroup, schedule);
        Assert.Equal(extraGroup.Schedule, schedule);
    }

    [Fact]
    public void ChangeGroup_GroupIsChanged()
    {
        ExtraGroup extraGroup1 = _isuServiceExtra.AddGroup("M32001");
        ExtraGroup extraGroup2 = _isuServiceExtra.AddGroup("M32021");
        ExtraStudent extraStudent = _isuServiceExtra.AddStudent(extraGroup1, "Emin");
        _isuServiceExtra.ChangeStudentGroup(extraStudent, extraGroup2);
        Assert.Equal(extraGroup2.Group, extraStudent.Student.Group);
    }

    [Fact]
    public void ChangeGroup_ExtraStudyDivisionIsChanged()
    {
        ExtraGroup extraGroup1 = _isuServiceExtra.AddGroup("M32001");
        ExtraGroup extraGroup2 = _isuServiceExtra.AddGroup("M32021");
        Teacher teacher = _isuServiceExtra.AddTeacher("Shrek");
        ExtraStudent extraStudent = _isuServiceExtra.AddStudent(extraGroup1, "Emin");
        _isuServiceExtra.ChangeScheduleToGroup(extraGroup1, CreateSchedule1());
        _isuServiceExtra.ChangeScheduleToGroup(extraGroup2, CreateSchedule2());
        ExtraStudy extraStudy = _isuServiceExtra.AddExtraStudy("cyber security", "CTU");
        var lesson1 = new Lesson(teacher, 302, DayOfWeek.Friday, 4);
        var lesson2 = new Lesson(teacher, 302, DayOfWeek.Thursday, 4);
        ExtraStudyDivision extraStudyDivision1 = _isuServiceExtra.AddExtraStudyDivision("K1.3", extraStudy, lesson1);
        ExtraStudyDivision extraStudyDivision2 = _isuServiceExtra.AddExtraStudyDivision("K1.2", extraStudy, lesson2);
        _isuServiceExtra.SubscribeStudent(extraStudent, extraStudyDivision1);
        _isuServiceExtra.ChangeStudentGroup(extraStudent, extraGroup2);
        Assert.NotEmpty(extraStudent.Divisions.ToList());
    }

    [Fact]
    public void ChangeSchedule_ExtraStudyDivisionIsChanged()
    {
        ExtraGroup extraGroup1 = _isuServiceExtra.AddGroup("M32001");
        Teacher teacher = _isuServiceExtra.AddTeacher("Shrek");
        ExtraStudent extraStudent = _isuServiceExtra.AddStudent(extraGroup1, "Emin");
        _isuServiceExtra.ChangeScheduleToGroup(extraGroup1, CreateSchedule1());
        ExtraStudy extraStudy = _isuServiceExtra.AddExtraStudy("cyber security", "CTU");
        var lesson1 = new Lesson(teacher, 302, DayOfWeek.Friday, 4);
        var lesson2 = new Lesson(teacher, 302, DayOfWeek.Thursday, 4);
        ExtraStudyDivision extraStudyDivision1 = _isuServiceExtra.AddExtraStudyDivision("K1.3", extraStudy, lesson1);
        ExtraStudyDivision extraStudyDivision2 = _isuServiceExtra.AddExtraStudyDivision("K1.2", extraStudy, lesson2);
        _isuServiceExtra.SubscribeStudent(extraStudent, extraStudyDivision1);
        _isuServiceExtra.ChangeScheduleToGroup(extraGroup1, CreateSchedule2());
        Assert.NotEmpty(extraStudent.Divisions.ToList());
    }

    private Schedule CreateSchedule1()
    {
        Teacher teacher1 = _isuServiceExtra.AddTeacher("Oleg");
        Teacher teacher2 = _isuServiceExtra.AddTeacher("Dora");
        return Schedule.Builder
            .AddLesson(new Lesson(teacher1, 229, DayOfWeek.Saturday, 2))
            .AddLesson(new Lesson(teacher1, 229, DayOfWeek.Saturday, 3))
            .AddLesson(new Lesson(teacher2, 331, DayOfWeek.Saturday, 4))
            .AddLesson(new Lesson(teacher2, 331, DayOfWeek.Saturday, 5))
            .Build();
    }

    private Schedule CreateSchedule2()
    {
        Teacher teacher3 = _isuServiceExtra.AddTeacher("Limar");
        Teacher teacher4 = _isuServiceExtra.AddTeacher("Dora");
        return Schedule.Builder
            .AddLesson(new Lesson(teacher3, 229, DayOfWeek.Friday, 2))
            .AddLesson(new Lesson(teacher3, 229, DayOfWeek.Friday, 3))
            .AddLesson(new Lesson(teacher4, 331, DayOfWeek.Friday, 4))
            .AddLesson(new Lesson(teacher4, 331, DayOfWeek.Friday, 5))
            .Build();
    }
}