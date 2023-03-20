using Backups.Visitors;

namespace Backups.RepositoryItems;

public class RepositoryFolder : IRepositoryFolder
{
    private readonly Func<IReadOnlyCollection<IRepositoryItem>> _itemsFactory;
    private readonly string _fullPath;

    public RepositoryFolder(string name, string fullPath, Func<IReadOnlyCollection<IRepositoryItem>> itemsFactory)
    {
        _fullPath = fullPath;
        _itemsFactory = itemsFactory;
        Name = name;
    }

    public string Name { get; }

    public void Accept(IRepositoryItemVisitor repositoryItemVisitor)
    {
        repositoryItemVisitor.Visit(this);
    }

    public IReadOnlyCollection<IRepositoryItem> GetRepositoryItems()
    {
        return _itemsFactory.Invoke();
    }
}