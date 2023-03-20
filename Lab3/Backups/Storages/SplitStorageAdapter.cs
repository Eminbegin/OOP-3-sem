using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Storages;

public class SplitStorageAdapter : IStorage
{
    private readonly IReadOnlyCollection<IStorage> _storages;

    public SplitStorageAdapter(IReadOnlyCollection<IStorage> storages, IRepository repository)
    {
        _storages = storages;
        Repository = repository;
    }

    public IRepository Repository { get; }

    public IEnumerable<IRepositoryItem> GetItems()
    {
        return _storages.SelectMany(amogus => amogus.GetItems());
    }

    public override string ToString()
    {
        return _storages.First().ToString() ?? "(Storage Empty)";
    }
}