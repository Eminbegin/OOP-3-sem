using Isu.Exceptions;

namespace Isu.Models;

public class GroupNumber
{
    private const int GroupNumberStartIndex = 2;
    private const int GroupNumberLength = 2;
    private const int MinGroupValue = 0;
    private const int MaxGroupValue = 99;

    public GroupNumber(string name)
    {
        Value = GetCroupNumber(name);
    }

    public int Value { get; }

    private int GetCroupNumber(string name)
    {
        if (!int.TryParse(name.Substring(GroupNumberStartIndex, GroupNumberLength), out int groupNumber))
        {
            throw new ParseGroupNumberException(name);
        }

        if (groupNumber is not(>= MinGroupValue and <= MaxGroupValue))
        {
            throw new InvalidGroupNumberException(groupNumber);
        }

        return groupNumber;
    }
}