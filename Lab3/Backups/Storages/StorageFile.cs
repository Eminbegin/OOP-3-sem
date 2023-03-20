using System.IO.Compression;
using Backups.RepositoryItems;
using Backups.Visitors;

namespace Backups.Storages;

public class StorageFile : IRepositoryFile
{
    private readonly ZipArchiveEntry _zipArchiveEntry;

    public StorageFile(ZipArchiveEntry zipArchiveEntry, string name)
    {
        _zipArchiveEntry = zipArchiveEntry;
        Name = name;
    }

    public string Name { get; }

    public Stream GetStream()
    {
        return _zipArchiveEntry.Open();
    }

    public void Accept(IRepositoryItemVisitor repositoryItemVisitor)
    {
        repositoryItemVisitor.Visit(this);
    }
}