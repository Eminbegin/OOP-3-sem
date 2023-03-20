using Backups.Visitors;

namespace Backups.RepositoryItems;

public interface IRepositoryFile : IRepositoryItem
{
    Stream GetStream();
}