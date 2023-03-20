using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Storages;

public interface IStorage
{
    IRepository Repository { get; }
    IEnumerable<IRepositoryItem> GetItems();
}