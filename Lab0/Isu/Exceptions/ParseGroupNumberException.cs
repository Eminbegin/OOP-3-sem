namespace Isu.Exceptions;

public class ParseGroupNumberException : IsuException
{
    public ParseGroupNumberException(string name)
        : base($"Can't parse {name} and get group number") { }
}