using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.Storages;

public class ZipArchivedFolder : IArchivedItem
{
    private readonly List<IArchivedItem> _childItems;
    private readonly string _name;
    public ZipArchivedFolder(string name, List<IArchivedItem> childItems)
    {
        _childItems = childItems;
        _name = name;
    }

    public IRepositoryItem GetStorageItem(ZipArchiveEntry zipArchiveEntry)
    {
        var zipArchive = new ZipArchive(zipArchiveEntry.Open(), ZipArchiveMode.Read);
        return new StorageFolder(
            new List<IRepositoryItem>(zipArchive.Entries
                .Select(e => _childItems
                    .First(i => i.GetName().Equals(e.Name)).GetStorageItem(e))), _name);
    }

    public string GetName() => _name;
}