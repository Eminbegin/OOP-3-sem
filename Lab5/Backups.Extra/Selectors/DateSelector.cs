using Backups.Entities;
using Backups.Extra.Exceptions;

namespace Backups.Extra.Selectors;

public class DateSelector : ISelector
{
    private readonly DateTime _date;

    public DateSelector(DateTime date)
    {
        _date = date;
    }

    public List<RestorePoint> SelectPoints(List<RestorePoint> points)
    {
        if (points.Count == 0)
        {
            throw SelectorsExceptions.BackupIsEmpty();
        }

        return points.Where(p => p.DateTime < _date).ToList();
    }
}