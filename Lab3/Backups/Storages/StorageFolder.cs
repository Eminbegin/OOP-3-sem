using Backups.RepositoryItems;
using Backups.Visitors;

namespace Backups.Storages;

public class StorageFolder : IRepositoryFolder
{
    private readonly IReadOnlyCollection<IRepositoryItem> _items;

    public StorageFolder(IReadOnlyCollection<IRepositoryItem> items, string name)
    {
        _items = items;
        Name = name;
    }

    public string Name { get; }

    public void Accept(IRepositoryItemVisitor repositoryItemVisitor)
    {
        repositoryItemVisitor.Visit(this);
    }

    public IReadOnlyCollection<IRepositoryItem> GetRepositoryItems() => _items;
}