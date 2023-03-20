using Backups.Entities;
using Backups.Extra.Cleaning;
using Backups.Extra.Selectors;

namespace Backups.Extra.Entities;

public class ExtraBackup : IBackup
{
    private readonly IBackup _backup;
    private readonly IResolver _resolver;
    private readonly ISelector _selector;

    public ExtraBackup(IBackup backup, IResolver resolver, ISelector selector)
    {
        if (backup is ExtraBackup)
        {
            throw new Exception();
        }

        _backup = backup;
        _resolver = resolver;
        _selector = selector;
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        _backup.AddRestorePoint(restorePoint);
        _resolver.Resolve(_backup, _selector.SelectPoints(_backup.RestorePoints().ToList()));
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        _backup.RemoveRestorePoint(restorePoint);
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints()
    {
        return _backup.RestorePoints();
    }
}