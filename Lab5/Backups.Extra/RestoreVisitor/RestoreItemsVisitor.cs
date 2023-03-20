using Backups.Repositories;
using Backups.RepositoryItems;
using Backups.Visitors;

namespace Backups.Extra.RestoreVisitor;

public class RestoreItemsVisitor : IRepositoryItemVisitor
{
    private readonly Stack<string> _paths;
    private readonly IRepository _repository;

    public RestoreItemsVisitor(IRepository repository)
    {
        _repository = repository;
        _paths = new Stack<string>();
    }

    public void Visit(IRepositoryFile item)
    {
        string fullPath = $"{_paths.Peek()}{item.Name}";

        if (_repository.IsItemExists(fullPath))
        {
            _repository.DeleteItem(fullPath);
        }

        using Stream newFile = _repository.GetStream(fullPath);
        using Stream source = item.GetStream();
        source.CopyTo(newFile);
    }

    public void Visit(IRepositoryFolder item)
    {
        string fullPath = $"{_paths.Peek()}{item.Name}{_repository.Separator}";
        _paths.Push(fullPath);

        if (_repository.IsItemExists(fullPath))
        {
            _repository.DeleteItem(fullPath);
        }

        foreach (IRepositoryItem newItem in item.GetRepositoryItems())
        {
            newItem.Accept(this);
        }

        _paths.Pop();
    }
}