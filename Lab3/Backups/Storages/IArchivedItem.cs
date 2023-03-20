using System.IO.Compression;
using Backups.RepositoryItems;

namespace Backups.Storages;

public interface IArchivedItem
{
    string GetName();
    IRepositoryItem GetStorageItem(ZipArchiveEntry zipArchiveEntry);
}