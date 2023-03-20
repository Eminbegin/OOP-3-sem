using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Isu.Utility;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService _isuService = new IsuService(new IdGenerator());

    [Theory]
    [InlineData("M3100", "Aboba Abobavich")]
    [InlineData("M32340", "Mem Memov")]
    [InlineData("P4222", "Dora")]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string groupName, string studentName)
    {
        Group group = _isuService.AddGroup(new GroupName(groupName));
        Student student = _isuService.AddStudent(group, studentName);
        Assert.Equal(group, student.Group);
        Assert.Contains(student, group.Students);
    }

    [Theory]
    [InlineData("M3100", "Aboba Abobavich")]
    [InlineData("M32340", "Mem Memov")]
    public void ReachMaxStudentPerGroup_ThrowException(string groupName, string studentName)
    {
        Group group = _isuService.AddGroup(new GroupName(groupName));
        for (int i = 0; i < group.StudentLimit; ++i)
        {
            _isuService.AddStudent(group, studentName);
        }

        Assert.Throws<ExceededLimitStudentsException>(() => _isuService.AddStudent(group, "Terentiev Alexandr"));
    }

    [Theory]
    [InlineData("M013100")]
    [InlineData("P56310c")]
    [InlineData("G423")]
    [InlineData("123415")]
    public void CreateGroupWithInvalidName_ThrowException(string name)
    {
        Assert.ThrowsAny<IsuException>(() => _isuService.AddGroup(new GroupName(name)));
    }

    [Theory]
    [InlineData("M3100", "M32340", "Aboba Abobavich")]
    [InlineData("M32340", "P4222", "Mem Memov")]
    [InlineData("P4222", "M3100", "Dora")]
    public void TransferStudentToAnotherGroup_GroupChanged(string groupName1, string groupName2, string studentName)
    {
        Group group = _isuService.AddGroup(new GroupName(groupName1));
        Group group1 = _isuService.AddGroup(new GroupName(groupName2));
        Student student = _isuService.AddStudent(group, studentName);
        _isuService.ChangeStudentGroup(student, group1);
        Assert.Equal(group1, student.Group);
    }
}