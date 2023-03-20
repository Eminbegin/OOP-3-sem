using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.RestoreVisitor;
using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Extra.Restorers;

public class CringeRestore : IRestorer
{
    private readonly IRepository _repository;

    public CringeRestore(IRepository repository)
    {
        _repository = repository;
    }

    public void Restore(RestorePoint restorePoint)
    {
        var items = restorePoint.Storage.GetItems().ToList();

        var visitor = new RestoreItemsVisitor(_repository);
        foreach (IBackupItem backupItem in restorePoint.Items)
        {
            IRepositoryItem? item = items.First(i => i.Name.Equals(backupItem.Name));

            if (item is null)
            {
                throw BackupExtraExceptions.NullItem();
            }

            item.Accept(visitor);
        }
    }
}