using Isu.Models;

namespace Isu.Extra.Exceptions;

public class IsuServiceExtraException : Exception
{
    private IsuServiceExtraException(string message)
        : base(message)
    {
    }

    public static IsuServiceExtraException StudentNotExist(int id)
        => new IsuServiceExtraException($"There is no student with id = {id}");

    public static IsuServiceExtraException TeacherNotExist(int id)
        => new IsuServiceExtraException($"There is no teacher with id = {id}");

    public static IsuServiceExtraException GroupNotExist(GroupName groupName)
        => new IsuServiceExtraException($"There is no group with name = \"{groupName.Name}\"");

    public static IsuServiceExtraException InvalidMegaFaculty(GroupName groupName)
        => new IsuServiceExtraException($"There are not MegaFaculty for group with name \"{groupName.Name}\"");

    public static IsuServiceExtraException InvalidMegaFaculty(string name)
        => new IsuServiceExtraException($"There are not MegaFaculty with name \"{name}\"");
}