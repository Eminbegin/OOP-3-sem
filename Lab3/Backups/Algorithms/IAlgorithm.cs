using Backups.Archivers;
using Backups.Entities;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Algorithms;

public interface IAlgorithm
{
    IStorage Save(string folderName, string archiveName, IReadOnlyCollection<IRepositoryItem> items, IRepository repository, IArchiver archiver);
}