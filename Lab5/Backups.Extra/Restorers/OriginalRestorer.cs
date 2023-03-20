using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.RestoreVisitor;
using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Extra.Restorers;

public class OriginalRestorer : IRestorer
{
    public void Restore(RestorePoint restorePoint)
    {
        var items = restorePoint.Storage.GetItems().ToList();

        foreach (IBackupItem backupItem in restorePoint.Items)
        {
            IRepositoryItem? item = items.First(i => i.Name.Equals(backupItem.Name));

            if (item is null)
            {
                throw BackupExtraExceptions.NullItem();
            }

            var visitor = new RestoreItemsVisitor(backupItem.Repository);
            item.Accept(visitor);
        }
    }
}