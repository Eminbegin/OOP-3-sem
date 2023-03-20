using Backups.Visitors;

namespace Backups.RepositoryItems;

public class RepositoryFile : IRepositoryFile
{
    private readonly Func<Stream> _streamCreator;
    private readonly string _fullPath;

    public RepositoryFile(string name, string fullPath, Func<Stream> streamCreator)
    {
        Name = name;
        _fullPath = fullPath;
        _streamCreator = streamCreator;
    }

    public string Name { get; }

    public Stream GetStream()
    {
        return _streamCreator.Invoke();
    }

    public void Accept(IRepositoryItemVisitor repositoryItemVisitor)
    {
        repositoryItemVisitor.Visit(this);
    }
}