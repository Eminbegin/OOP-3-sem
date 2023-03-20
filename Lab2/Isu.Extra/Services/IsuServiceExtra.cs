using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;
using Isu.Utility;

namespace Isu.Extra.Services;

public class IsuServiceExtra : IIsuServiceExtra
{
    private IsuService _isuService;
    private ExtraStudyService _extraStudyService;
    private IdGenerator _idGenerator;
    private List<ExtraStudent> _students;
    private List<ExtraGroup> _groups;
    private List<Teacher> _teachers;
    private IReadOnlyCollection<MegaFaculty> _megaFaculties;

    public IsuServiceExtra()
    {
        var idGenerator = new IdGenerator();
        List<MegaFaculty> megaFaculties = GetMegaFaculties();
        _megaFaculties = megaFaculties;
        _isuService = new IsuService(idGenerator);
        _extraStudyService = new ExtraStudyService(megaFaculties);
        _idGenerator = idGenerator;
        _students = new List<ExtraStudent>();
        _teachers = new List<Teacher>();
        _groups = new List<ExtraGroup>();
    }

    public ExtraGroup AddGroup(string name)
    {
        Group group = _isuService.AddGroup(new GroupName(name));

        var extraGroup = new ExtraGroup(group);
        _groups.Add(extraGroup);
        return extraGroup;
    }

    public ExtraStudent AddStudent(ExtraGroup extraGroup, string name)
    {
        Student student = _isuService.AddStudent(extraGroup.Group, name);

        var extraStudent = new ExtraStudent(student, GetMegaFaculty(extraGroup.Group.GroupName));
        _students.Add(extraStudent);
        return extraStudent;
    }

    public Teacher AddTeacher(string name)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible add teacher with null Name");
        var teacher = new Teacher(name, _idGenerator.Next());
        _teachers.Add(teacher);
        return teacher;
    }

    public ExtraStudent? FindExtraStudent(int id) =>
        _students.FirstOrDefault(s => s.Student.IsuId.Equals(id));

    public ExtraStudent GetExtraStudent(int id)
    {
        ExtraStudent? extraStudent = FindExtraStudent(id);
        if (extraStudent is null)
        {
            throw IsuServiceExtraException.StudentNotExist(id);
        }

        return extraStudent;
    }

    public Teacher? FindTeacher(int id) =>
        _teachers.FirstOrDefault(t => t.IsuId.Equals(id));

    public Teacher GetTeacher(int id)
    {
        Teacher? teacher = FindTeacher(id);
        if (teacher is null)
        {
            throw IsuServiceExtraException.TeacherNotExist(id);
        }

        return teacher;
    }

    public ExtraGroup? FindExtraGroup(GroupName groupName) =>
        _groups.FirstOrDefault(g => g.Group.GroupName.Equals(groupName));

    public ExtraGroup GetExtraGroup(GroupName groupName)
    {
        ExtraGroup? extraGroup = FindExtraGroup(groupName);
        if (extraGroup is null)
        {
            throw IsuServiceExtraException.GroupNotExist(groupName);
        }

        return extraGroup;
    }

    public void ChangeScheduleToGroup(ExtraGroup extraGroup, Schedule schedule)
    {
        ArgumentNullException.ThrowIfNull(extraGroup, "Impossible add null schedule to group");
        ArgumentNullException.ThrowIfNull(schedule, "Impossible add schedule to null group");
        extraGroup.ChangeSchedule(schedule);
        foreach (Student student in extraGroup.Group.Students)
        {
            ExtraStudent extraStudent = GetExtraStudentByStudent(student);
            foreach (ExtraStudyDivision division in extraStudent.Divisions.ToList())
            {
                ResolveIntersection(extraStudent, extraGroup, division);
            }
        }
    }

    public void SubscribeStudentToExtraStudy(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision)
    {
        ArgumentNullException.ThrowIfNull(extraStudent, "Impossible add null student to extra study");
        ArgumentNullException.ThrowIfNull(extraStudyDivision, "Impossible add student to null extra study");
        StudentExist(extraStudent);
        if (extraStudent.GetDivisionsCount() == 2 ||
            extraStudent.Divisions
                .Any(d => d.ExtraStudy.Equals(extraStudyDivision.ExtraStudy)) ||
            extraStudent.MegaFaculty.Equals(extraStudyDivision.ExtraStudy.MegaFaculty))
        {
            throw ExtraStudyServiceException.StudentCantHaveThisDivision(extraStudent, extraStudyDivision);
        }

        _extraStudyService.SubscribeStudent(extraStudent, extraStudyDivision, GetGroupByStudent(extraStudent).Schedule);
    }

    public void ChangeStudentGroup(ExtraStudent extraStudent, ExtraGroup extraGroup)
    {
        _isuService.ChangeStudentGroup(extraStudent.Student, extraGroup.Group);
        foreach (ExtraStudyDivision division in extraStudent.Divisions.ToList())
        {
            ResolveIntersection(extraStudent, extraGroup, division);
        }
    }

    public ExtraStudy AddExtraStudy(string name, string megaFacultyName)
    {
        MegaFaculty megaFaculty = GetMegaFacultyByName(megaFacultyName);
        ArgumentNullException.ThrowIfNull(name, "Impossible add extra study with null name");
        return _extraStudyService.AddExtraStudy(name, megaFaculty);
    }

    public ExtraStudyDivision AddExtraStudyDivision(string name, ExtraStudy extraStudy, Lesson lesson)
    {
        ArgumentNullException.ThrowIfNull(name, "Impossible add division of extra study with null name");
        ArgumentNullException.ThrowIfNull(extraStudy, "Impossible add division of null extra study");
        ArgumentNullException.ThrowIfNull(lesson, "Impossible add division of extra study with null lesson");
        return _extraStudyService.AddExtraStudyDivision(name, extraStudy, lesson);
    }

    public void SubscribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision)
    {
        ArgumentNullException.ThrowIfNull(extraStudent, "Impossible add null student to extra study");
        ArgumentNullException.ThrowIfNull(extraStudyDivision, "Impossible add student to null division of extra study");

        _extraStudyService.SubscribeStudent(extraStudent, extraStudyDivision, GetGroupByStudent(extraStudent).Schedule);
    }

    public void DescribeStudent(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision)
    {
        ArgumentNullException.ThrowIfNull(extraStudent, "Impossible remove null student to extra study");
        ArgumentNullException.ThrowIfNull(extraStudyDivision, "Impossible remove student from null division of extra study");

        _extraStudyService.DescribeStudent(extraStudent, extraStudyDivision);
    }

    public IReadOnlyCollection<ExtraStudyDivision> GetExtraStudyDivisions(string name)
        => _extraStudyService.GetExtraStudyDivisions(name);

    public IReadOnlyCollection<ExtraStudent> GetStudentsFromDivision(string name)
        => _extraStudyService.GetStudentsFromDivision(name);

    public IReadOnlyCollection<ExtraStudent> GetStudentsWithOutExtraStudy()
        => _students.Where(s => s.GetDivisionsCount() < 2).ToList();

    private MegaFaculty GetMegaFaculty(GroupName groupName)
    {
        MegaFaculty? megaFaculty =
            _megaFaculties.FirstOrDefault(m => m.AvailableGroups.Contains(groupName.FacultyNumber.Value));
        if (megaFaculty is null)
        {
            throw IsuServiceExtraException.InvalidMegaFaculty(groupName);
        }

        return megaFaculty;
    }

    private void StudentExist(ExtraStudent extraStudent)
    {
        if (!_students.Contains(extraStudent))
        {
            throw IsuServiceExtraException.StudentNotExist(extraStudent.Student.IsuId);
        }
    }

    private ExtraGroup GetGroupByStudent(ExtraStudent extraStudent)
    {
        return _groups
            .First(g => g.Group.Students
                .Contains(extraStudent.Student));
    }

    private MegaFaculty GetMegaFacultyByName(string name)
    {
        MegaFaculty? megaFaculty = _megaFaculties.FirstOrDefault(m => m.Name.Equals(name.ToUpper()));
        if (megaFaculty is null)
        {
            throw IsuServiceExtraException.InvalidMegaFaculty(name);
        }

        return megaFaculty;
    }

    private void ResolveIntersection(ExtraStudent extraStudent, ExtraGroup extraGroup, ExtraStudyDivision extraStudyDivision)
    {
        if (!_extraStudyService.IsExtraStudyNotIntersect(extraStudent, extraGroup.Schedule, extraStudyDivision))
        {
            _extraStudyService.ChangeIntersectionExtraStudy(extraStudent, extraGroup.Schedule, extraStudyDivision);
        }
    }

    private ExtraStudent GetExtraStudentByStudent(Student student)
    {
        return _students.First(es => es.Student.Equals(student));
    }

    private List<MegaFaculty> GetMegaFaculties()
    {
        MegaFaculty[] result =
        {
            new MegaFaculty("TINT", "MKJ"),
            new MegaFaculty("CTU", "RPNH"),
            new MegaFaculty("FTMI", "U"),
            new MegaFaculty("BTINS", "GTO"),
            new MegaFaculty("FT", "LZV"),
        };
        return result.ToList();
    }
}