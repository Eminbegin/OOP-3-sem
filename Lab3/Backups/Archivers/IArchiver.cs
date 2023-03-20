using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Archivers;

public interface IArchiver
{
    IStorage CreateNew(
        IRepository repository,
        string name,
        IReadOnlyCollection<IRepositoryItem> items,
        string folderName);
}