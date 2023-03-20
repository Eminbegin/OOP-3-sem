namespace Backups.RepositoryItems;

public interface IRepositoryFolder : IRepositoryItem
{
    IReadOnlyCollection<IRepositoryItem> GetRepositoryItems();
}