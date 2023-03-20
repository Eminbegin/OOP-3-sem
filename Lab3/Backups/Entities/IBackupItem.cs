using Backups.Repositories;
using Backups.RepositoryItems;

namespace Backups.Entities;

public interface IBackupItem
{
    string Name { get; }
    IRepository Repository { get; }
    IRepositoryItem GetRepositoryItem();
    string GetIdentifier();
}