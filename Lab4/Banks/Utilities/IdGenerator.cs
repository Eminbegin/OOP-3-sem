namespace Banks.Utilities;

public class IdGenerator
{
    private const int MinValue = 0;
    private const int MaxValue = 100000;

    private int _lastId;

    public IdGenerator(int startValue = MinValue)
    {
        _lastId = startValue;
    }

    public int Next()
    {
        _lastId++;
        if (_lastId == MaxValue)
        {
            throw new Exception();
        }

        return _lastId;
    }
}