using Backups.Storages;

namespace Backups.Entities;

public class SimpleBackup : IBackup
{
    private readonly List<RestorePoint> _restorePoints;

    public SimpleBackup()
    {
        _restorePoints = new List<RestorePoint>();
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (!_restorePoints.Remove(restorePoint))
        {
            throw new Exception();
        }
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints() => _restorePoints.AsReadOnly();

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }

    public IStorage GetLastStorage()
    {
        return _restorePoints[^1].Storage;
    }
}