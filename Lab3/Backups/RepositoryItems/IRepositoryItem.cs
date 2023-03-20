using System.Runtime.CompilerServices;
using Backups.Visitors;

namespace Backups.RepositoryItems;

public interface IRepositoryItem
{
    string Name { get; }
    void Accept(IRepositoryItemVisitor repositoryItemVisitor);
}