using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class ExtraStudyServiceException : Exception
{
    private ExtraStudyServiceException(string message)
        : base(message)
    {
    }

    public static ExtraStudyServiceException ExtraStudyAlreadyExist()
        => new ExtraStudyServiceException("Student already has extra study");

    public static ExtraStudyServiceException ExtraStudyAlreadyExist(string name)
        => new ExtraStudyServiceException($"There is extra study with name \"{name}\"");

    public static ExtraStudyServiceException ExtraStudyNotExist(ExtraStudy extraStudy)
        => new ExtraStudyServiceException($"There is not extra study with name \"{extraStudy.Name}\"");

    public static ExtraStudyServiceException ExtraStudyHasIntersection(ExtraStudyDivision extraStudyDivision, ExtraStudent extraStudent)
        => new ExtraStudyServiceException(
            $"Student with id = {extraStudent.Student.IsuId} has intersection with division of extra study with name \"{extraStudyDivision.Name}\"");

    public static ExtraStudyServiceException StudentAlreadyHasNotExtraStudy(ExtraStudent extraStudent)
        => new ExtraStudyServiceException($"Student with id = {extraStudent.Student.IsuId} already hasn't extra study");

    public static ExtraStudyServiceException StudentCantHaveThisDivision(ExtraStudent extraStudent, ExtraStudyDivision extraStudyDivision)
        => new ExtraStudyServiceException($"Student with id {extraStudent.Student.IsuId} can't have division with name \"{extraStudyDivision.Name}\"");

    public static ExtraStudyServiceException ExtraStudyNotExist(string name)
        => new ExtraStudyServiceException($"There is not extra study with name \"{name}\"");
}