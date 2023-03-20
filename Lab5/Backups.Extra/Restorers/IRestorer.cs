using Backups.Entities;

namespace Backups.Extra.Restorers;

public interface IRestorer
{
    void Restore(RestorePoint restorePoint);
}