using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class InvalidExtraStudyException : Exception
{
    private InvalidExtraStudyException(string message)
        : base(message)
    {
    }

    public static InvalidExtraStudyException DivisionAlreadyExist(ExtraStudyDivision extraStudyDivision)
        => new InvalidExtraStudyException(
            $"Extra study \"{extraStudyDivision.ExtraStudy.Name}\" already has division \"{extraStudyDivision.Name}\"");

    public static InvalidExtraStudyException DivisionContainsStudent(ExtraStudyDivision extraStudyDivision, ExtraStudent extraStudent)
        => new InvalidExtraStudyException(
            $"Division of extra study with name \"{extraStudyDivision.Name}\" already contains student with id {extraStudent.Student.IsuId}");

    public static InvalidExtraStudyException DivisionNotContainsStudent(ExtraStudyDivision extraStudyDivision, ExtraStudent extraStudent)
        => new InvalidExtraStudyException(
            $"Division of extra study with name \"{extraStudyDivision.Name}\" already doesn't contain student with id {extraStudent.Student.IsuId}");
}