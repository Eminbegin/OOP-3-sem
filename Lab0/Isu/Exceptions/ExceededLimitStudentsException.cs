using Isu.Models;

namespace Isu.Exceptions;

public class ExceededLimitStudentsException : IsuException
{
    public ExceededLimitStudentsException(GroupName groupName)
        : base($"Group with name {groupName.Name} is full") { }
}