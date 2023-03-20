using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Storages;

public class ZipArchiveStorage : IStorage
{
    private readonly IReadOnlyCollection<IArchivedItem> _items;
    private readonly string _folderName;
    private readonly string _zipArchiveName;

    public ZipArchiveStorage(IReadOnlyCollection<IArchivedItem> items, IRepository repository, string folderName, string zipArchiveName)
    {
        _items = items;
        Repository = repository;
        _folderName = folderName;
        _zipArchiveName = zipArchiveName;
    }

    public IRepository Repository { get; }

    public override string ToString()
    {
        return $"(Repository: {Repository}, folderName: {_folderName})";
    }

    public IEnumerable<IRepositoryItem> GetItems()
    {
        var zipArchive = new ZipArchive(Repository.GetStream(_folderName, _zipArchiveName));
        return zipArchive.Entries
            .Select(e => _items
                .First(i => i.GetName().Equals(e.Name))
                .GetStorageItem(e));
    }
}