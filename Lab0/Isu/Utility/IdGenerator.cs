using Isu.Exceptions;

namespace Isu.Utility;

public class IdGenerator
{
    public const int MinValue = 100000;
    public const int MaxValue = 999999;

    private int _lastId;

    public IdGenerator()
    {
        _lastId = MinValue;
    }

    public int Next()
    {
        _lastId++;
        if (_lastId > MaxValue)
        {
            throw new IdLimitReachedException();
        }

        return _lastId;
    }
}