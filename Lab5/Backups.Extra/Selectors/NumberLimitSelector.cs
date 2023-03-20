using Backups.Entities;
using Backups.Extra.Exceptions;

namespace Backups.Extra.Selectors;

public class NumberLimitSelector : ISelector
{
    private readonly int _limit;

    public NumberLimitSelector(int limit)
    {
        if (limit <= 0)
        {
            throw SelectorsExceptions.LimitValue();
        }

        _limit = limit;
    }

    public List<RestorePoint> SelectPoints(List<RestorePoint> points)
    {
        return points.OrderBy(p => p.DateTime).Take(points.Count - _limit).ToList();
    }
}