using Backups.Entities;

namespace Backups.Extra.Cleaning;

public interface IResolver
{
    void Resolve(IBackup backup, List<RestorePoint> points);
}