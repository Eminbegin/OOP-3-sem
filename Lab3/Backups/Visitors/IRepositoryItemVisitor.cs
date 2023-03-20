using Backups.RepositoryItems;
using Backups.Storages;

namespace Backups.Visitors;

public interface IRepositoryItemVisitor
{
    void Visit(IRepositoryFile item);
    void Visit(IRepositoryFolder item);
}