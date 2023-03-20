using Backups.Archivers;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Algorithms;

public class SingleAlgorithm : IAlgorithm
{
    public IStorage Save(
        string folderName,
        string archiveName,
        IReadOnlyCollection<IRepositoryItem> items,
        IRepository repository,
        IArchiver archiver)
    {
        return archiver.CreateNew(repository, archiveName, items, folderName);
    }
}