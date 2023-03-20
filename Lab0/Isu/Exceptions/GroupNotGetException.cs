using Isu.Models;

namespace Isu.Exceptions;

public class GroupNotGetException : IsuException
{
    public GroupNotGetException(GroupName groupName)
        : base($"There is not group with Name {groupName.Name}") { }
}