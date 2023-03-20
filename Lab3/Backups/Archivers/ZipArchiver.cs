using System.IO.Compression;
using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Storages;
using Backups.Visitors;

namespace Backups.Archivers;

public class ZipArchiver : IArchiver
{
    private readonly string _type;
    public ZipArchiver(string type)
    {
        _type = type;
    }

    public IStorage CreateNew(IRepository repository, string name, IReadOnlyCollection<IRepositoryItem> items, string folderName)
    {
        repository.CreateFolder(folderName);
        Stream memoryStream = repository.GetStream($"{name}{_type}");
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create);
        var visitor = new ArchiveRepositoryItemVisitor(archive, _type);
        foreach (IRepositoryItem item in items)
        {
            item.Accept(visitor);
        }

        archive.Dispose();
        return new ZipArchiveStorage(visitor.GetArchivedItems(), repository, folderName, $"{name}{_type}");
    }
}