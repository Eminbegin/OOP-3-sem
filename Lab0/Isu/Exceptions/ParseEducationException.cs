namespace Isu.Exceptions;

public class ParseEducationException : IsuException
{
    public ParseEducationException(string name)
        : base($"Can't parse {name} and get education value") { }
}