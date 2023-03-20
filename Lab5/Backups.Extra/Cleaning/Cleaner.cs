using Backups.Entities;

namespace Backups.Extra.Cleaning;

public class Cleaner : IResolver
{
    public void Resolve(IBackup backup, List<RestorePoint> points)
    {
        foreach (RestorePoint point in points)
        {
            backup.RemoveRestorePoint(point);
        }
    }
}