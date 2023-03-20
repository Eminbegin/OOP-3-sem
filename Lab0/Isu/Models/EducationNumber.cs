using Isu.Exceptions;

namespace Isu.Models;

public class EducationNumber
{
    private const int EducationStageIndex = 1;
    private const int MinEducationValue = 3;
    private const int MaxEducationValue = 4;

    public EducationNumber(string name)
    {
        Value = GetEducationNumber(name);
    }

    public int Value { get; }

    private int GetEducationNumber(string name)
    {
        if (!int.TryParse(name[EducationStageIndex].ToString(), out int educationNumber))
        {
            throw new ParseEducationException(name);
        }

        if (educationNumber is not(>= MinEducationValue and <= MaxEducationValue))
        {
            throw new InvalidEducationException(educationNumber);
        }

        return educationNumber;
    }
}