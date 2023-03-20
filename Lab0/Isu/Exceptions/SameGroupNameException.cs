using Isu.Models;

namespace Isu.Exceptions;

public class SameGroupNameException : Exception
{
    public SameGroupNameException(GroupName groupName)
    : base($"There is group with name {groupName.Name}") { }
}