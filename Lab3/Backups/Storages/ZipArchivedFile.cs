using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.Storages;

public class ZipArchivedFile : IArchivedItem
{
    private readonly string _name;
    public ZipArchivedFile(string name)
    {
        _name = name;
    }

    public IRepositoryItem GetStorageItem(ZipArchiveEntry zipArchiveEntry)
    {
        return new StorageFile(zipArchiveEntry, _name);
    }

    public string GetName() => _name;
}