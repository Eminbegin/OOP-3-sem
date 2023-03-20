using System.IO.Compression;
using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Visitors;

public class ArchiveRepositoryItemVisitor : IRepositoryItemVisitor
{
    private readonly Stack<List<IArchivedItem>> _items;
    private readonly Stack<ZipArchive> _archives;
    private readonly string _type;

    public ArchiveRepositoryItemVisitor(ZipArchive zipArchive, string type)
    {
        _archives = new Stack<ZipArchive>();
        _archives.Push(zipArchive);
        _items = new Stack<List<IArchivedItem>>();
        _items.Push(new List<IArchivedItem>());
        _type = type;
    }

    public void Visit(IRepositoryFile item)
    {
        ZipArchiveEntry entry = _archives.Peek().CreateEntry(item.Name);
        using Stream entryStream = entry.Open();
        using Stream source = item.GetStream();
        source.CopyTo(entryStream);

        _items.Peek().Add(new ZipArchivedFile(item.Name));
    }

    public void Visit(IRepositoryFolder item)
    {
        _items.Push(new List<IArchivedItem>());

        ZipArchiveEntry entry = _archives.Peek().CreateEntry($"{item.Name}{_type}");
        Stream entryStream = entry.Open();
        using var zipArchive = new ZipArchive(entryStream, ZipArchiveMode.Create);
        _archives.Push(zipArchive);

        foreach (IRepositoryItem newItem in item.GetRepositoryItems())
        {
            newItem.Accept(this);
        }

        _archives.Pop();
        List<IArchivedItem> saveListItems = _items.Pop();
        _items.Peek().Add(new ZipArchivedFolder($"{item.Name}{_type}", saveListItems));
    }

    public IReadOnlyCollection<IArchivedItem> GetArchivedItems() => _items.Peek();
}