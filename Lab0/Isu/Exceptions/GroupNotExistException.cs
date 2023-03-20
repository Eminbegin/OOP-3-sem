using Isu.Models;

namespace Isu.Exceptions;

public class GroupNotExistException : IsuException
{
    public GroupNotExistException(GroupName groupName)
        : base($"There is not Group with name {groupName.Name}") { }
}