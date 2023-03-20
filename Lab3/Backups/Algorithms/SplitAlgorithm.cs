using Backups.Archivers;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Algorithms;

public class SplitAlgorithm : IAlgorithm
{
    public IStorage Save(
        string folderName,
        string archiveName,
        IReadOnlyCollection<IRepositoryItem> items,
        IRepository repository,
        IArchiver archiver)
    {
        return new SplitStorageAdapter(
            items
                .Select(item => Map(item, archiver, repository, folderName))
                .ToList(),
            repository);
    }

    private IStorage Map(
        IRepositoryItem item,
        IArchiver archiver,
        IRepository repository,
        string folderName)
    {
        return archiver.CreateNew(
                repository,
                item.Name,
                new List<IRepositoryItem> { item },
                folderName);
    }
}