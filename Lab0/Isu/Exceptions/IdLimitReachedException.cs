namespace Isu.Exceptions;

public class IdLimitReachedException : IsuException
{
    public IdLimitReachedException()
        : base("Reached limit of IsuId") { }
}